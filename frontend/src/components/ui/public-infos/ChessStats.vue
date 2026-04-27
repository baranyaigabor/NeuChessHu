<script setup>
import { computed } from "vue"
import { useI18n } from '@utils/i18n'

const props = defineProps({
    whiteMatches: { type: Array, default: () => [] },
    blackMatches: { type: Array, default: () => [] },
    myId: { type: Number, required: true },
})

const { t } = useI18n()
const allMatches = computed(() => [...props.whiteMatches, ...props.blackMatches])

function getMostCommon(arr) 
{
    if (!arr.length)
    {
        return "N/A"
    }

    const freq = arr.reduce((acc, val) => ((acc[val] = (acc[val] || 0) + 1), acc), {})
    return Object.entries(freq).sort((a, b) => b[1] - a[1])[0][0]
}

function timeToSeconds(t) 
{
    if (!t)
    {
        return null
    }

    const parts = t.split(":").map(Number)

    if (parts.length === 3)
    {
        return parts[0] * 3600 + parts[1] * 60 + parts[2]
    }

    if (parts.length === 2) 
    {
        return parts[0] * 60 + parts[1]
    }

    return null
}

function secondsToTime(seconds) 
{
    const minutes = Math.floor(seconds / 60).toString().padStart(2, "0")
    const secs = Math.floor(seconds % 60).toString().padStart(2, "0")
    return `${minutes}:${secs}`
}

const favouriteType = computed(() => 
    getMostCommon(allMatches.value.map((x) => x.gamemode).filter(Boolean))
)

const mostPlayedDuration = computed(() => 
    getMostCommon(allMatches.value.map((x) => x.match_durations).filter(Boolean))
)

const favouriteFirstMove = computed(() => 
{
    const firstMoves = allMatches.value.map((x) => {
        try 
        {
            const moves = typeof x.moves === "string" 
                ? JSON.parse(x.moves) 
                : x.moves

            if (!moves?.length)
            {
                return null
            }

            const isWhite = x.white_id === props.myId

            return isWhite ? moves[0]?.white : moves[0]?.black
        } 
        
        catch 
        { 
            return null 
        }
    }).filter(Boolean)

    return getMostCommon(firstMoves)
})

const avgWinningTime = computed(() => 
{
    const winTimes = allMatches.value
        .filter((x) => Number(x.winner_id) === props.myId && x.winner_time)
        .map((x) => timeToSeconds(x.winner_time))
        .filter((x) => x !== null)

    if (!winTimes.length)
    {
        return "N/A"
    }

    return secondsToTime(winTimes.reduce((a, b) => a + b, 0) / winTimes.length)
})

const stats = computed(() => 
[
    { key: 'favouriteType', label: t('match.favouriteType'), value: favouriteType.value },
    { key: 'mostPlayedDuration', label: t('match.mostPlayedDuration'), value: mostPlayedDuration.value },
    { key: 'favouriteFirstMove', label: t('match.favouriteFirstMove'), value: favouriteFirstMove.value },
    { key: 'avgWinningTime', label: t('match.avgWinningTime'), value: avgWinningTime.value },
])
</script>

<template>
    <div class="flex w-full flex-col">
        <div class="grid grid-cols-1 gap-2 sm:grid-cols-2 xl:grid-cols-4">
            <div v-for="stat in stats" :key="stat.label" class="h-full min-h-0 bg-[var(--ButtonBrush)] border !border-black rounded p-2.5 flex flex-col items-center justify-center text-center gap-1">
                <span class="text-[10px] text-[var(--TextBrush)] font-medium uppercase tracking-wide leading-tight">
                    {{ stat.label }}
                </span>

                <span class="text-sm font-bold px-3 my-2 py-0.5 rounded-full text-center" :class="stat.key === 'favouriteType' ? {
                        '!bg-[#F0D0A0] text-[#7A4A00]': stat.value === 'Bullet',
                        '!bg-[#D0E0F0] text-[#1A4A70]': stat.value === 'Blitz',
                        '!bg-[#D0EDD0] text-[#1A5A1A]': stat.value === 'Rapid',
                    } : 'bg-(--ButtonBrush) text-(--TextBrush)'">
                    {{ stat.value }}
                </span>
            </div>
        </div>
    </div>
</template>