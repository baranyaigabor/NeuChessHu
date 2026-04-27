<script setup>
import { computed, ref, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import { useI18n } from '@utils/i18n'
import { X } from 'lucide-vue-next'

const props = defineProps({
    whiteMatches: {
        type: Array,
        default: () => []
    },
    blackMatches: {
        type: Array,
        default: () => []
    },
    myId: {
        type: [Number, String],
        default: 1
    }
})

const nicknameCache = ref({})
const avatarCache = ref({})
const { t, locale } = useI18n()

const allMatches = computed(() => 
{
    const white = (props.whiteMatches ?? []).map(x => ({ ...x, myColor: 'white' }))
    const black = (props.blackMatches ?? []).map(x => ({ ...x, myColor: 'black' }))
    return [...white, ...black].sort((a, b) => new Date(b.played_at) - new Date(a.played_at))
})

onMounted(async () => 
{
    const opponentIds = [...new Set(allMatches.value.map(x => getOpponentId(x)))]
    
    await Promise.all(
        opponentIds.map(async (id) => 
        {
            if (!id || nicknameCache.value[id])
            {
                return
            }

            try 
            {
                const response = await api.get(`users/${id}`)
                nicknameCache.value[id] = response.data?.data?.nickname ?? `#${id}`
                
                const pic = response.data?.data?.profile_picture
                avatarCache.value[id] = pic === 'Unknown' 
                    ? null 
                    : pic 
                    ?? null
            } 

            catch 
            {
                nicknameCache.value[id] = `#${id}`
                avatarCache.value[id] = null
            }
        })
    )
})

function getOpponentAvatar(match) 
{
    const id = getOpponentId(match)
    return avatarCache.value[id] ?? null
}

function getOpponentId(match) 
{
    return match?.myColor === 'white' ? match?.black_id : match?.white_id
}

function getOpponentName(match) 
{
    const id = getOpponentId(match)
    return nicknameCache.value[id] ?? `#${id}`
}

function getResult(match) 
{
    if (!match?.winner_id || match.winner_id === 'Unknown' || match.winner_id === null)
    {
        return 'unknown'
    }

    return String(match.winner_id) === String(props.myId) 
        ? 'win' 
        : 'loss'
}

function formatDate(dateStr) 
{
    if (!dateStr)
    {
        return '—'
    }

    return dateStr.slice(0, 10)
}

function formatTime(duration, locale) 
{
    if (!duration)
    {
        return '—'
    }

    if (duration.includes('|'))
    {
        return duration
    }

    return locale === 'hu'
        ? `${duration} perc`
        : `${duration} min`
}

function formatMatchResult(match) 
{
    const result = match

    if (!result)
    {
        return "—"
    }

    const hu = locale.value === "hu"

    switch (result) 
    {
        case "Timeout":
            return hu ? "Lejárt az idő" : "By Timeout"

        case "Resign":
            return hu ? "Feladás végett" : "By Resignation"

        case "Checkmate":
            return hu ? "Sakkmatt által" : "By Checkmate"

        case "Stalemate":
            return hu ? "Patt helyzet" : "By Stalemate"

        case "Mutual Agreement":
            return hu ? "Közös megegyezéssel" : "By Mutual Agreement"

        case "Threefold-repetition":
            return hu ? "Háromszori lépésismétlés miatt" : "By Threefold-Repetition"

        case "Fivefold-repetition":
            return hu ? "Ötszöri lépésismétlés miatt" : "By Fivefold-Repetition"

        case "FiftyConsecutiveMoves":
            return hu ? "Ötvenlépés szabály miatt" : "By 50 Consecutive Moves"

        case "SeventyFiveConsecutiveMoves":
            return hu ? "Hetvenötlépés szabály miatt" : "By 75 Consecutive Moves"

        case "InsufficientMaterial":
            return hu ? "Lehetetlen állás miatt" : "By Insufficient Materials"

        default:
            return result
    }
}

function handleMatchClick(match) 
{
    matchFilterStore.selectMatch(match)
}
</script>

<template>
</template>