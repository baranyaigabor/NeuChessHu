<script setup>
import { computed, ref, onMounted, onUnmounted } from 'vue'
import { api } from '@utils/http.mjs'
import { useI18n } from '@utils/i18n'
import { X } from 'lucide-vue-next'
import defaultProfilePicDark from '@/assets/profile_pictures/ProfilePic_dark.png'
import defaultProfilePicLight from '@/assets/profile_pictures/ProfilePic_light.png'

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
const isDarkTheme = ref(document.documentElement.classList.contains('dark'))
const { t, locale } = useI18n()
let themeObserver = null

const defaultProfilePicture = computed(() =>
    isDarkTheme.value
        ? defaultProfilePicDark
        : defaultProfilePicLight
)

const allMatches = computed(() => 
{
    const white = (props.whiteMatches ?? []).map(x => ({ ...x, myColor: 'white' }))
    const black = (props.blackMatches ?? []).map(x => ({ ...x, myColor: 'black' }))
    return [...white, ...black].sort((a, b) => new Date(b.played_at) - new Date(a.played_at))
})

onMounted(async () => 
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
                avatarCache.value[id] = normalizeProfilePicture(pic)
            } 

            catch 
            {
                nicknameCache.value[id] = `#${id}`
                avatarCache.value[id] = null
            }
        })
    )
})

onUnmounted(() => 
{
    themeObserver?.disconnect()
})

function normalizeProfilePicture(pic)
{
    if (!pic || pic === 'Unknown')
    {
        return null
    }

    if (typeof pic === 'string' && pic.includes('data:image')) 
    {
        const match = pic.match(/(data:image\/[^;]+;base64,.+)/)
        return match ? match[1] : null
    }

    return pic
}

function getOpponentAvatar(match) 
{
    const id = getOpponentId(match)
    return avatarCache.value[id] ?? defaultProfilePicture.value
}

function isDefaultOpponentAvatar(match)
{
    return getOpponentAvatar(match) === defaultProfilePicture.value
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
    <div class="match-list max-h-[22rem] overflow-y-auto md:max-h-[10rem]">
        <div v-if="allMatches.length === 0" class="px-4 py-6 text-center text-[13px] text-(--TextBrush)">
            {{ t('match.noMatches') }}
        </div>

        <template v-else>
            <RouterLink v-for="(match, index) in allMatches" :key="match?.match_id ?? index" :to="`/user/${getOpponentId(match)}`"
                @click="handleMatchClick(match)" class="match-mobile flex flex-wrap items-center gap-2 px-2 py-2.5 hover:bg-black/[0.08] transition-colors duration-100 !no-underline sm:px-4" :class="{ 'border-b !border-[var(--BorderChangingBrush)]/[0.24]': index < allMatches.length - 1 }">
                <div class="match-avatar w-7 h-7 rounded-md overflow-hidden shrink-0" :class="{
                        '!border-3 !border-[var(--ChartPointWinBackgroundColor)]': getResult(match) === 'win',
                        '!border-3 !border-[var(--ChartPointLoseBackgroundColor)]': getResult(match) === 'loss',
                        '!border-3 !border-[var(--ChartPointDrawBackgroundColor)]': getResult(match) === 'unknown',
                    }" >
                    <img :src="getOpponentAvatar(match)" :alt="getOpponentName(match)" class="w-full h-full" :class="isDefaultOpponentAvatar(match) ? 'scale-[1.5] object-contain' : 'object-cover'"/>
                </div>

                <div class="match-main flex-1 min-w-[150px]">
                    <div class="text-[13px] font-semibold text-[var(--TextStrongBrush)]">
                        {{ t('match.vs') }} {{ getOpponentName(match) }}
                    </div>
                    <div class="text-[11px] text-[var(--TextMutedBrush)] mt-px">
                        {{ formatTime(match?.winner_time, locale) }} · {{ formatMatchResult(match?.match_end_result ?? '—') }}
                    </div>
                </div>

                <span class="match-result text-[12px] font-bold min-w-[36px] text-center shrink-0"
                    :class="{
                        'text-(--ChartPointWinBackgroundColor)': getResult(match) === 'win',
                        'text-(--ChartPointLoseBackgroundColor)': getResult(match) === 'loss',
                        'text-(--ChartPointDrawBackgroundColor)': getResult(match) === 'unknown',
                    }">
                    {{ getResult(match) === 'win' ? t('match.win') : getResult(match) === 'loss' ? t('match.loss') : t('match.draw') }}
                </span>

                <span v-if="match?.gamemode" class="match-mode text-[11px] font-semibold px-3 py-0.5 rounded-full shrink-0 min-w-[65px] text-center"
                    :class="{
                        'bg-[#F0D0A0] text-[#7A4A00]': match.gamemode === 'Bullet',
                        'bg-[#D0E0F0] text-[#1A4A70]': match.gamemode === 'Blitz',
                        'bg-[#D0EDD0] text-[#1A5A1A]': match.gamemode === 'Rapid',
                    }">
                    {{ match.gamemode }}
                </span>
                <span v-else class="match-mode text-[11px] text-[var(--TextBrush)] shrink-0">—</span>

                <span class="match-date text-[11px] text-[var(--TextBrush)] shrink-0 sm:min-w-[70px] sm:text-right">
                    {{ formatDate(match?.played_at) }}
                </span>
            </RouterLink>
        </template>
    </div>
</template>

<style lang="css">
.match-list {
    scrollbar-width: thin;
    scrollbar-color: transparent transparent;
    scrollbar-gutter: stable;
    padding-right: 0.7rem;
    margin-right: -0.2rem;
}

.match-list:hover,
.match-list:focus-within {
    scrollbar-color: var(--ScrollThumbBrush) transparent;
}

.match-list::-webkit-scrollbar {
    width: 6px;
}

.match-list::-webkit-scrollbar-track {
    background: transparent;
}

.match-list::-webkit-scrollbar-thumb {
    background: transparent;
    border-radius: 99px;
}

.match-list:hover::-webkit-scrollbar-thumb,
.match-list:focus-within::-webkit-scrollbar-thumb {
    background: var(--ScrollThumbBrush);
}

.match-list::-webkit-scrollbar-thumb:hover {
    background: var(--ScrollThumbHoverBrush);
}

@media (max-width: 526px) {
    .match-mobile {
        display: grid;
        grid-template-columns: auto minmax(140px, 1fr) auto;
        grid-template-areas:
            "avatar main main"
            "result mode date";
        align-items: center;
        justify-content: center;
        column-gap: 8px;
        row-gap: 6px;
        text-align: center;
    }

    .match-mobile .match-avatar { 
        grid-area: avatar; 
    }

    .match-mobile .match-main { 
        grid-area: main; min-width: 0; text-align: left; 
    }

    .match-mobile .match-result { 
        grid-area: result; justify-self: center; 
    }

    .match-mobile .match-mode {
        grid-area: mode; justify-self: center; 
    }

    .match-mobile .match-date {
        grid-area: date; justify-self: center; 
    }
}
</style>