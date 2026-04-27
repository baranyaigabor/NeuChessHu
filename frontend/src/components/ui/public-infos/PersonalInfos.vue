<script setup>
import RegionEdit from './RegionEdit.vue'
import NicknameEdit from './NicknameEdit.vue'
import DataOfBirthEdit from './DateOfBirthEdit.vue'
import FirstNameEdit from './FirstNameEdit.vue'
import LastNameEdit from './LastNameEdit.vue'
import { computed, ref, watch } from 'vue'
import defaultProfilePic from '@/assets/profile_pictures/ProfilePic_dark.png'
import { useI18n } from '@utils/i18n'
import { countryName } from '@utils/i18n/countries'

const props = defineProps({
    user: { type: Object, required: true },
    userId: { type: Number, default: null },
    isOwner: { type: Boolean, default: false },
})

watch(() => props.user, () => {
    isEditing.value = false
    editData.value = { ...props.user }
})

const emit = defineEmits(['save'])
const { locale, t } = useI18n()

const isEditing = ref(false)
const isDragging = ref(false)
const editData = ref({ ...props.user })

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
        return defaultProfilePic
    }

    if (pic.includes('data:image')) 
    {
        const match = pic.match(/(data:image\/[^;]+;base64,.+)/)
        return match ? match[1] : defaultProfilePic
    }

    return pic
})

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

const displayedRegion = computed(() => 
{
    const region = props.user.region

    if (!region || region === 'Unknown')
    {
        return t('common.unknown')
    }

    return region.includes('_') 
        ? countryName(region, locale.value) 
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
{}
</script>
