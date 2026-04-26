import { defineStore } from "pinia";
import { api } from "@utils/http.mjs";
import { ref, computed } from "vue"
import { t } from "@utils/i18n"

export const useUserStore = defineStore("api/user", () => {
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

    function resetRegistrationData() {
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

    function setCredentials({ email, password }) {
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
    
    async function login(data) {
        const response = await api.post('signin', data)
        const newToken = response.data.token

        user.value = { data: response.data.user }
        token.value = newToken
        userId.value = response.data.user.id
        return user.value.data
    }

    async function register(data) {
        const response = await api.post('users', data)

        user.value = response.data.data
        userId.value = response.data.data.id
        return response.data.data
    }

    async function logout() {
        try {
            await api.post("logout", null, {
                headers: {
                    Authorization: `Bearer ${token.value}`
                }
            })
        } catch (e) {
            console.error("Logout error:", e)
        }

        token.value = ""
        user.value = { role: "" }
        userId.value = null

        sessionStorage.removeItem("token")
        sessionStorage.removeItem("user")

        return t("auth.logoutSuccess")
    }

    async function updateUser(data) {
        const payload = { ...data }
        if (data.full_name) {
            const parts = data.full_name.trim().split(' ')
            payload.first_name = parts[0] || ''
            payload.last_name = parts.slice(1).join(' ') || ''
            delete payload.full_name
        }

        const response = await api.patch(`users/${user.value.data.nickname}`, payload, {
            headers: {
                Authorization: `Bearer ${token.value}`
            }
        })

        user.value = { ...user.value, data: response.data.data }
        return response.data
    }

    async function fetchUser(nickname) {
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
        fetchUser,
        logout 
    }
}, {
    persist: {
        storage: sessionStorage
    }
})
