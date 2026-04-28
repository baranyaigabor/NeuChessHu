<script setup>
import RegionEdit from './RegionEdit.vue'
import NicknameEdit from './NicknameEdit.vue'
import DataOfBirthEdit from './DateOfBirthEdit.vue'
import FirstNameEdit from './FirstNameEdit.vue'
import LastNameEdit from './LastNameEdit.vue'
import { computed, onMounted, onUnmounted, ref, watch } from 'vue'
import defaultProfilePicDark from '@/assets/profile_pictures/ProfilePic_dark.png'
import defaultProfilePicLight from '@/assets/profile_pictures/ProfilePic_light.png'
import { useI18n } from '@utils/i18n'
import { countryName, countryValueFromStoredName, countryValues } from '@utils/i18n/countries'

const props = defineProps({
    user: { type: Object, required: true },
    userId: { type: Number, default: null },
    isOwner: { type: Boolean, default: false },
})

watch(() => props.user, () => {
    isEditing.value = false
    editData.value = { ...props.user }
})

const emit = defineEmits(['save', 'delete'])
const { locale, t } = useI18n()

const isEditing = ref(false)
const isDragging = ref(false)
const showDeleteConfirm = ref(false)
const editData = ref({ ...props.user })
const isDarkTheme = ref(document.documentElement.classList.contains('dark'))
let themeObserver = null

const defaultProfilePicture = computed(() =>
    isDarkTheme.value
        ? defaultProfilePicDark
        : defaultProfilePicLight
)

onMounted(() => 
{
    themeObserver = new MutationObserver(() => 
    {
        isDarkTheme.value = document.documentElement.classList.contains('dark')
    })

    themeObserver.observe(document.documentElement, 
    {
        attributes: true,
        attributeFilter: ['class'],
    })
})

onUnmounted(() => 
{
    themeObserver?.disconnect()
})

const firstName = computed({
    get() {
        const parts = editData.value.full_name?.trim().split(' ') || []
        return parts[0] || ''
    },
    set(val) {
        const parts = editData.value.full_name?.trim().split(' ') || []
        parts[0] = val
        editData.value.full_name = parts.join(' ')
    }
})

const lastName = computed({
    get() 
    {
        const parts = editData.value.full_name?.trim().split(' ') || []
        return parts.slice(1).join(' ') || ''
    },

    set(val) 
    {
        const parts = editData.value.full_name?.trim().split(' ') || []
        parts.splice(1, parts.length - 1, val)
        editData.value.full_name = parts.join(' ')
    }
})

const cleanProfilePicture = computed(() => 
{
    const pic = isEditing.value 
        ? editData.value.profile_picture 
        : props.user.profile_picture

    if (!pic || pic === 'Unknown')
    {
        return defaultProfilePicture.value
    }

    if (typeof pic === 'string' && pic.includes('data:image')) 
    {
        const match = pic.match(/(data:image\/[^;]+;base64,.+)/)
        return match ? match[1] : defaultProfilePicture.value
    }

    return pic
})

const isDefaultProfilePicture = computed(() => cleanProfilePicture.value === defaultProfilePicture.value)

const formattedCreatedAt = computed(() => 
{
    if (!props.user.created_at)
    {
        return t('common.unknown')
    }

    const date = new Date(props.user.created_at)

    if (isNaN(date.getTime()))
    {
        return props.user.created_at
    }

    return date.toLocaleString(locale.value === 'hu' ? 'hu-HU' : 'en-US', 
    {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    })
})

const statusKey = computed(() => 
{
    const val = props.user.is_active

    if (val === true)
    {
        return 'online'
    }

    if (val === false)
    {
        return 'offline'
    }

    if (val === 'Online' || val === 'Offline')
    {
        return val.toLowerCase()
    }

    return 'unknown'
})

const normalizedStatus = computed(() => t(`common.${statusKey.value}`))

const localizedCountries = computed(() =>
    countryValues.map((value) => ({
        value,
        name: countryName(value, 'hu'),
        label: countryName(value, locale.value),
    }))
)

const displayedRegion = computed(() => 
{
    const region = props.user.region

    if (!region || region === 'Unknown')
    {
        return t('common.unknown')
    }

    const countryValue = countryValueFromStoredName(region, localizedCountries.value)

    return countryValue
        ? countryName(countryValue, locale.value)
        : region
})

function openSettings() 
{
    if (!props.isOwner)
    {
        return
    }

    editData.value = { ...props.user }
    isEditing.value = true
}

function cancelEdit() 
{
    isEditing.value = false
    showDeleteConfirm.value = false
    editData.value = { ...props.user }
}

function saveEdit() 
{
    const dataToSave = { ...editData.value }

    if (dataToSave.profile_picture && dataToSave.profile_picture !== 'Unknown' && !dataToSave.profile_picture.startsWith('data:')) 
    {
        dataToSave.profile_picture = `data:image/jpeg;base64,${dataToSave.profile_picture}`
    }

    emit('save', dataToSave)
    isEditing.value = false
}

function handleDrop(e) 
{
    isDragging.value = false
    processImage(e.dataTransfer.files[0])
}

function handleFileInput(e) 
{
    processImage(e.target.files[0])
}

function processImage(file) 
{
    if (!file)
    {
        return
    }

    if (!file.type.startsWith('image/'))
    {
        return alert(t('profile.imageOnly'))
    }

    const rawImage = new Image()
    const reader = new FileReader()

    reader.onload = (e) => { rawImage.src = e.target.result }

    rawImage.onload = () => 
    {
        const canvas = document.createElement('canvas')
        const maxPxSize = 512
        let { width, height } = rawImage

        if (width > height) 
        {
            if (width > maxPxSize) 
            {
                height *= maxPxSize / width
                width = maxPxSize
            }
        }

        else if (height > maxPxSize) 
        {
            width *= maxPxSize / height
            height = maxPxSize
        }

        canvas.width = width
        canvas.height = height
        const context = canvas.getContext('2d')
        context.fillStyle = getComputedStyle(document.documentElement).getPropertyValue('--background').trim() || '#ffffff'
        context.fillRect(0, 0, width, height)
        context.drawImage(rawImage, 0, 0, width, height)

        const base64 = canvas.toDataURL('image/jpeg', 0.8)
        if (base64.length > 1.5 * 1024 * 1024)
        {
            return alert(t('profile.imageTooLarge'))
        }
        editData.value.profile_picture = base64
    }

    reader.readAsDataURL(file)
}

function clearProfilePicture() 
{
    editData.value.profile_picture = null
}

function deleteAccount() 
{
    showDeleteConfirm.value = true
}

function cancelDelete()
{
    showDeleteConfirm.value = false
}

function confirmDelete()
{
    showDeleteConfirm.value = false
    emit('delete', props.user.nickname)
}
</script>

<template>
    <div class="relative grid h-auto min-h-0 w-full min-w-0 grid-cols-1 md:-gap-2 lg:gap-5 overflow-visible rounded-xl px-4 py-4 pr-12 text-left md:grid-cols-2 md:px-6 md:pr-14 lg:grid-cols-[minmax(160px,0.7fr)_minmax(0,1.15fr)_minmax(0,1.15fr)] lg:items-stretch"
        :class="isEditing && isOwner ? 'items-start' : 'items-center'">
        
        <div class="absolute right-5.5 top-4.5 flex gap-2">
            <template v-if="isEditing && isOwner">
                <button class="text-green-600 transition hover:text-green-400" @click="saveEdit" :title="t('common.save')">
                    <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.2">
                        <path d="M20 7 9 18l-5-5"/>
                    </svg>
                </button>
                <button class="text-red-600 transition hover:text-red-400" @click="cancelEdit" :title="t('common.cancel')">
                    <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round">
                        <path d="M18 6 6 18M6 6l12 12"/>
                    </svg>
                </button>
                <button class="text-red-800 transition hover:text-red-500" @click="deleteAccount" :title="t('common.deleteAccount')">
                    <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                        <path d="M3 6h18M10 6v12m4-12v12M5 6l1 14a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2l1-14M9 3h6l1 2H8l1-2z" />
                    </svg>
                </button>
            </template>
            <button v-else-if="isOwner" class="pe-3 translate-y-[0.2rem] text-(--TextBrush) hover:text-gray-600 translate-x-[0.9rem] transition" @click="openSettings">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                    <path d="M12 15.5A3.5 3.5 0 1 0 12 8.5a3.5 3.5 0 0 0 0 7Z"/>
                    <path d="M19.4 15a1.7 1.7 0 0 0 .34 1.87l.06.06a2 2 0 1 1-2.83 2.83l-.06-.06A1.7 1.7 0 0 0 15 19.4a1.7 1.7 0 0 0-1 .26 1.7 1.7 0 0 0-.85 1.47V21a2 2 0 1 1-4 0v-.09a1.7 1.7 0 0 0-.85-1.47 1.7 1.7 0 0 0-1-.26 1.7 1.7 0 0 0-1.87.34l-.06.06a2 2 0 1 1-2.83-2.83l.06-.06A1.7 1.7 0 0 0 4.6 15a1.7 1.7 0 0 0-.26-1 1.7 1.7 0 0 0-1.47-.85H2.8a2 2 0 1 1 0-4h.09a1.7 1.7 0 0 0 1.47-.85 1.7 1.7 0 0 0 .26-1 1.7 1.7 0 0 0-.34-1.87l-.06-.06a2 2 0 1 1 2.83-2.83l.06.06A1.7 1.7 0 0 0 9 4.6a1.7 1.7 0 0 0 1-.26 1.7 1.7 0 0 0 .85-1.47V2.8a2 2 0 1 1 4 0v.09a1.7 1.7 0 0 0 .85 1.47 1.7 1.7 0 0 0 1 .26 1.7 1.7 0 0 0 1.87-.34l.06-.06a2 2 0 1 1 2.83 2.83l-.06.06A1.7 1.7 0 0 0 19.4 9c0 .35.09.7.26 1 .3.5.84.81 1.43.85h.11a2 2 0 1 1 0 4h-.09a1.7 1.7 0 0 0-1.47.85c-.17.3-.24.65-.24 1z"/>
                </svg>
            </button>

            <Teleport to="body">
                <div v-if="showDeleteConfirm" class="fixed inset-0 z-50 flex items-center justify-center">
                    <div class="absolute inset-0 bg-black/40 backdrop-blur-sm" @click="cancelDelete"></div>
                    <div class="relative z-10 w-72 rounded-lg border border-(--BorderChangingBrush)! bg-(--SideBarBrush) p-3 text-sm text-(--TextBrush) shadow-2xl">
                        <p class="mb-4 leading-snug text-lg text-center font-semibold">
                            {{ t('profile.deleteAccountConfirm') }}
                        </p>

                        <div class="grid grid-cols-2 justify-end gap-6 h-12 px-3">
                            <button type="button" class="rounded bg-(--ButtonBrush) border border-(--BorderChangingBrush)! px-3 py-1 text-xs transition hover:bg-(--ButtonBrush)/70" @click="cancelDelete">
                                {{ t('common.cancel') }}
                            </button>

                            <button type="button" class="rounded border border-(--BorderChangingBrush)! bg-red-700 px-3 py-1 text-xs text-white transition hover:bg-red-800" @click="confirmDelete">
                                {{ t('common.deleteAccount') }}
                            </button>
                        </div>
                    </div>
                </div>
            </Teleport>
        </div>

        <div class="info-card-col-left flex min-w-0 flex-col items-center! justify-center! md:col-span-2 lg:col-span-1 lg:self-stretch lg:pr-6">
            <div class="mt-3 flex h-28 w-28 shrink-0 items-center! justify-center! overflow-visible text-2xl font-semibold"
                :class="isEditing && isOwner ? 'relative cursor-pointer rounded-full border-2 border-dashed border-gray-400 bg-gray-100 transition' : 'rounded-full'"
                @dragover.prevent="isEditing && isOwner && (isDragging = true)" @dragleave="isEditing && isOwner && (isDragging = false)"
                @drop.prevent="isEditing && isOwner && handleDrop($event)" @click="isEditing && isOwner && $refs.fileInput.click()">
                
                <div v-if="!isEditing || !isOwner" class="h-full w-full overflow-hidden rounded-full">
                    <img :src="cleanProfilePicture" :alt="user.nickname || user.full_name" class="h-full w-full object-cover object-center" :class="isDefaultProfilePicture ? 'scale-[1.44]' : ''" />
                </div>
                <template v-else>
                    <div class="relative h-full w-full overflow-visible rounded-full">
                        <div v-if="editData.profile_picture && editData.profile_picture !== 'Unknown'" class="h-full w-full overflow-hidden rounded-full">
                            <img :src="cleanProfilePicture" :alt="user.nickname || user.full_name" class="h-full w-full object-cover object-center" :class="isDefaultProfilePicture ? 'scale-[1.44]' : ''" />
                        </div>
                        
                        <div v-else class="flex h-full w-full flex-col items-center! justify-center! px-1 text-center text-xs leading-tight text-slate-900">
                            <div class="text-lg">📁</div>
                            <div>{{ t('profile.dropHere') }}</div>
                            <div class="m-2 text-slate-700">{{ t('profile.max2Mb') }}</div>
                        </div>

                        <button class="absolute -right-1 -top-1.5 z-10 text-red-600 transition hover:text-red-400" @click.stop="clearProfilePicture" :title="t('common.cancel')">
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round">
                                <path d="M18 6 6 18M6 6l12 12"/>
                            </svg>
                        
                        </button>
                    </div>
                </template>

                <input v-if="isEditing && isOwner" ref="fileInput" type="file" accept="image/*" class="hidden" @change="handleFileInput" />
            </div>

            <div :class="['flex min-h-[2.6rem] w-full items-center! justify-center!', isEditing && isOwner ? 'mt-3' : 'mt-[0.61rem]']">                <NicknameEdit v-if="isEditing && isOwner" class="nickname-input pt-1" :value="editData.nickname" @nicknameChange="editData.nickname = $event"/>
                <p v-else-if="user.nickname" class="profile-value nickname-display no-ellipsis-scroll pt-1 text-center text-xl">@{{ user.nickname }}</p>
            </div>
        </div>

        <div class="info-card-col-main flex w-full min-w-0 flex-col items-center justify-center! space-y-4 border-t border-(--RuleBrush)! pt-4 mt-3 md:border-t-0 lg:border-l lg:pl-6! lg:pt-0" style="border-color: var(--RuleBrush) !important;">
            <div class="info-row">
                <h6 class="info-label">{{ t('common.region') }}:</h6>
                <RegionEdit v-if="isEditing && isOwner" class="info-input" :value="editData.region" @regionChange="editData.region = $event" />
                <span v-else class="profile-value no-ellipsis-scroll font-medium">{{ displayedRegion }}</span>
            </div>

            <div class="info-row">
                <h6 class="info-label">{{ t('common.dateOfBirth') }}:</h6>
                <DataOfBirthEdit v-if="isEditing && isOwner" class="info-input info-input-date" v-model="editData.date_of_birth" />
                <span v-else class="profile-value value-date">
                    {{ user.date_of_birth && user.date_of_birth !== 'Unknown' ? user.date_of_birth : t('common.unknown') }}
                </span>
            </div>
        </div>

        <div class="info-card-col-main flex w-full min-w-0 flex-col items-center justify-center! space-y-4 border-t border-(--RuleBrush)! pt-4 mt-3 md:border-t-0 lg:border-l lg:pl-6! lg:pt-0" style="border-color: var(--RuleBrush) !important;">
            <div v-if="isEditing && isOwner" class="w-full space-y-4">
                <div class="info-row">
                    <h6 class="info-label">{{ t('common.firstName') }}:</h6>
                    <FirstNameEdit class="info-input" :value="firstName" @firstNameChange="firstName = $event"/>
                </div>
                <div class="info-row">
                    <h6 class="info-label">{{ t('common.lastName') }}:</h6>
                    <LastNameEdit class="info-input" :value="lastName" @lastNameChange="lastName = $event"/>
                </div>
            </div>

            <div v-else class="w-full space-y-4">
                <div class="info-row">
                    <h6 class="info-label">{{ t('profile.registeredAt') }}:</h6>
                    <span class="profile-value no-ellipsis-scroll font-medium">{{ formattedCreatedAt }}</span>
                </div>
                <div class="info-row">
                    <h6 class="info-label">{{ t('profile.status') }}:</h6>
                    <span class="no-ellipsis-scroll font-medium" :class="{
                            'text-(--StatusWinBrush)!': statusKey === 'online',
                            'text-(--StatusLossBrush)!': statusKey === 'offline',
                            'text-(--StatusUnknownBrush)': statusKey === 'unknown'}">
                        {{ normalizedStatus }}
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="css">
.profile-value {
    color: var(--ProfileValueBrush) !important;
}

.no-ellipsis-scroll {
    min-width: 0;
    max-width: 100%;
    white-space: nowrap;
    overflow-x: auto;
    overflow-y: hidden;
    text-overflow: clip;
    -ms-overflow-style: none;
    scrollbar-width: none;
}

.no-ellipsis-scroll::-webkit-scrollbar {
    display: none;
}

.info-row {
    display: grid;
    grid-template-columns: clamp(7.5rem, 34%, 11rem) minmax(0, 1fr);
    align-items: center;
    gap: 0.9rem;
    min-height: 2.5rem;
    min-width: 0;
    width: 100%;
}

.info-label {
    color: var(--TextBrush);
    display: flex;
    align-items: center;
    min-height: 2.5rem;
    margin: 0;
    min-width: 0;
    text-align: left;
    transform: translateY(0.12rem);
    white-space: nowrap;
}

.nickname-input,
.nickname-display {
    width: min(100%, 14rem);
    min-width: 0;
    margin-inline: auto;
}

.nickname-input :deep(input) {
    text-align: center;
}

.info-input,
.info-input-date {
    display: flex;
    align-items: center;
    width: 100%;
    min-width: 0;
}

.info-input :deep(input),
.info-input :deep(select),
.info-input :deep(button),
.info-input-date :deep(input),
.info-input-date :deep(select),
.info-input-date :deep(button) {
    width: 100%;
    min-width: 0;
}

.info-input-date :deep(button) {
    display: flex;
    align-items: center;
    gap: 0.6rem;
}

.info-value, 
.info-row > span {
    display: inline-block;
    position: relative;
    top: 0.13rem;
}

.info-card-col-main {
    border-color: var(--RuleBrush) !important; 
}

@media (min-width: 768px) {
    .info-row {
        grid-template-columns: clamp(10rem, 34%, 13rem) minmax(0, 1fr);
    }
}

@media (min-width: 768px) and (max-width: 864px) {
    .info-row {
        grid-template-columns: clamp(8rem, 28%, 10rem) minmax(0, 1fr);
        gap: 0.55rem;
        padding-right: 1rem;
    }
}

@media (min-width: 865px) and (max-width: 1024px) {
    .info-row {
        padding-right: 1rem;
    }
}

@media (min-width: 865px) and (max-width: 1199px) {
    .info-row {
        grid-template-columns: clamp(8.5rem, 30%, 10.5rem) minmax(0, 1fr);
        gap: 0.55rem;
    }
}

@media (min-width: 1200px) {
    .info-row {
        grid-template-columns: clamp(6.5rem, 24%, 8rem) minmax(0, 1fr);
        gap: 0.45rem;
        justify-items: stretch;
    }
}

@media (min-width: 1024px){
    .nickname-input :deep(input) {
        text-align: left;
    }
}

@media (min-width: 300px) and (max-width: 863px) {
    .info-card-col-main {
        text-align: left;
        align-items: center;
    }

    .info-row {
        justify-items: stretch;
    }
}

@media (min-width: 865px) {
    .info-card-col-left {
        min-width: 0;
    }

    .info-card-col-main {
        min-width: 0;
        overflow: visible;
    }
}
</style>
