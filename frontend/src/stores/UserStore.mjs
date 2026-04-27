import { defineStore } from "pinia";
import { api } from "@utils/http.mjs";
import { ref, computed } from "vue"
import { locale, t } from "@utils/i18n"
import { countryName, countryValueFromStoredName, countryValues } from "@utils/i18n/countries"

const countryOptions = countryValues.map((value) => ({
    value,
    name: countryName(value, 'hu')
}))

function normalizeRegionForStorage(value)
{
    if (!value || value === 'Unknown')
    {
        return null
    }

    const countryValue = countryValueFromStoredName(value, countryOptions)

    return countryValue
        ? countryName(countryValue, locale.value)
        : value
}

function normalizeUserPayload(data) 
{
    const payload = {}

    if (data.nickname && data.nickname !== 'Unknown') 
    {
        payload.nickname = data.nickname
    }

    if (data.full_name) 
    {
        const parts = data.full_name.trim().split(' ')
        payload.first_name = parts[0] || ''
        payload.last_name = parts.slice(1).join(' ') || ''
    } 
    else 
    {
        if (data.first_name !== undefined) 
        {
            payload.first_name = data.first_name === 'Unknown' ? null : data.first_name
        }

        if (data.last_name !== undefined)
        {
            payload.last_name = data.last_name === 'Unknown' ? null : data.last_name
        }
    }

    if (data.region !== undefined) 
    {
        payload.region = normalizeRegionForStorage(data.region)
    }

    if (data.date_of_birth !== undefined) 
    {
        payload.date_of_birth = data.date_of_birth === 'Unknown' || data.date_of_birth === '' ? null : data.date_of_birth
    }

    if (isValidImageDataUrl(data.profile_picture)) 
    {
        payload.profile_picture = data.profile_picture
    }

    return payload
}

function isValidImageDataUrl(value) 
{
    if (typeof value !== 'string')
    {
        return false
    }

    const match = value.match(/^data:image\/(jpe?g|png);base64,([A-Za-z0-9+/]+={0,2})$/)
    
    if (!match)
    {
        return false
    }

    try 
    {
        const bytes = atob(match[2])

        const isPng = bytes.charCodeAt(0) === 0x89 &&
                      bytes.slice(1, 4) === 'PNG'

        const isJpeg = bytes.charCodeAt(0) === 0xff &&
                       bytes.charCodeAt(1) === 0xd8 &&
                       bytes.charCodeAt(2) === 0xff

        return isPng || isJpeg
    } 
    
    catch 
    {
        return false
    }
}

function normalizeUsers(responseData) 
{
    if (Array.isArray(responseData))
    {
        return responseData
    }

    if (Array.isArray(responseData?.data))
    {
        return responseData.data
    }

    return []
}

function normalizeAuthenticatedUser(user)
{
    if (!user)
    {
        return null
    }

    return {
        id: user.id,
        nickname: user.nickname,
        email: user.email,
        role: user.role
    }
}

export const useUsersStore = defineStore('users', () => 
{
    async function getUsers() 
    {
        const response = await api.get('users', {
            headers: {
                Authorization: `Bearer ${useUserStore().token}`
            }
        })

        const users = normalizeUsers(response.data)

        return Promise.all(
            users.map(async (user) => 
            {
                if (!user.nickname)
                {
                    return user
                }

                try 
                {
                    const detailsResponse = await api.get(`users/${user.nickname}`)

                    return {
                        ...user,
                        ...detailsResponse.data.data
                    }
                } 

                catch 
                {
                    return user
                }
            })
        )
    }

    async function updateUser(identifier, data) 
    {
        const response = await api.patch(`users/${identifier}`, normalizeUserPayload(data), {
            headers: {
                Authorization: `Bearer ${useUserStore().token}`
            }
        })

        return response.data.data
    }

    return {
        getUsers,
        updateUser
    }
})

export const useUserStore = defineStore("user", () => 
{
    const token = ref("")
    const user = ref({})
    const userId = ref(null)

    const registrationData = ref({
        nickname: "",
        email: "",
        password: "",
        first_name: "",
        last_name: "",
        region: "",
        date_of_birth: "",
        profile_picture: null
    });

    function resetRegistrationData() 
    {
        registrationData.value = {
            nickname: "",
            email: "",
            password: "",
            first_name: "",
            last_name: "",
            region: "",
            date_of_birth: "",
            profile_picture: null
        }
    }

    function setCredentials({ email, password }) 
    {
        registrationData.value.email = email;
        registrationData.value.password = password;
    }

    function setPersonalInformations({ first_name, last_name, nickname, region, date_of_birth }) {
        registrationData.value.first_name = first_name;
        registrationData.value.last_name = last_name;
        registrationData.value.nickname = nickname;
        registrationData.value.region = region;
        registrationData.value.date_of_birth = date_of_birth;
    }

    const getRegistrationData = computed(() => registrationData.value);
    
    async function login(data) 
    {
        const response = await api.post('signin', data)
        const newToken = response.data.token
        const authenticatedUser = normalizeAuthenticatedUser(response.data.user)

        user.value = { data: authenticatedUser }
        token.value = newToken
        userId.value = authenticatedUser?.id ?? null
        return user.value.data
    }

    async function register(data) 
    {
        const registerResponse = await api.post('users', {
            ...data,
            region: normalizeRegionForStorage(data.region)
        })

        const loginResponse = await api.post('signin', {
            email: data.email,
            password: data.password
        })

        const newToken = loginResponse.data.token
        const authenticatedUser = normalizeAuthenticatedUser(loginResponse.data.user)

        user.value = { data: authenticatedUser }
        token.value = newToken
        userId.value = authenticatedUser?.id ?? registerResponse.data.data?.id ?? null

        return user.value.data
    }

    async function logout() 
    {
        if (token.value) 
        {
            try 
            {
                await api.post("logout", null, {
                    headers: {
                        Authorization: `Bearer ${token.value}`
                    }
                })
            } 
            
            catch (e) 
            {
                console.error("Logout error:", e)
            }
        }

        token.value = ""
        user.value = { role: "" }
        userId.value = null

        sessionStorage.removeItem("token")
        sessionStorage.removeItem("user")

        return t("auth.logoutSuccess")
    }

    async function deleteUser(id) 
    {
        const currentUser = user.value?.data ?? user.value
        const isDeletingSelf = String(currentUser?.id) === String(id)
        const isAdmin = currentUser?.role === 'admin'

        if (!id || !token.value)
        {
            return
        }

        const identifier = isDeletingSelf
            ? (currentUser?.nickname ?? id)
            : id

        try
        {
            await api.delete(`users/${identifier}`, {
                headers: {
                    Authorization: `Bearer ${token.value}`
                }
            })
        }
        catch (e)
        {
            if (e?.response?.status !== 404)
            {
                throw e
            }
        }

        if (isDeletingSelf && !isAdmin)
        {
            token.value = ""
            user.value = { role: "" }
            userId.value = null

            sessionStorage.removeItem("token")
            sessionStorage.removeItem("user")
        }
    }

    async function deleteCurrentUser() 
    {
        const currentUser = user.value?.data ?? user.value

        return deleteUser(currentUser?.id)
    }

    async function updateUser(data) 
    {
        const response = await api.patch(`users/${user.value.data.nickname}`, normalizeUserPayload(data), {
            headers: {
                Authorization: `Bearer ${token.value}`
            }
        })

        user.value = { ...user.value, data: response.data.data }

        return response.data
    }

    async function fetchUser(nickname)
    {
        const response = await api.get(`users/${nickname}`)
        return response.data.data
    }

    return { 
        token,
        user,
        userId,
        registrationData,
        getRegistrationData,
        setCredentials,
        setPersonalInformations,
        resetRegistrationData,
        login,
        register,
        updateUser,
        deleteUser,
        deleteCurrentUser,
        fetchUser,
        logout 
    }
}, {
    persist: {
        storage: sessionStorage
    }
})