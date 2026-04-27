<script setup>
import { Donut } from "@unovis/ts";
import { computed, ref, onMounted, onUnmounted, watch } from 'vue';
import { VisDonut, VisSingleContainer } from "@unovis/vue";
import { ChartContainer, ChartTooltip, ChartTooltipContent, componentToString } from "@/components/ui/chart";
import { Chart, DoughnutController, ArcElement, LineController, LineElement, PointElement, LinearScale, CategoryScale, Tooltip, Filler, RadarController, RadialLinearScale } from 'chart.js';
import { useI18n } from '@utils/i18n'

Chart.register(DoughnutController, ArcElement, LineController, LineElement, PointElement, LinearScale, CategoryScale, Tooltip, Filler, RadarController, RadialLinearScale)

const props = defineProps({
    whiteMatches: { type: Array, default: () => [] },
    blackMatches: { type: Array, default: () => [] },
    myId: { type: [Number, String]}
})

const { locale, t } = useI18n()

const allMatches = computed(() => [
    ...props.whiteMatches,
    ...props.blackMatches,
])

const lineRef = ref(null)
let lineChart = null
const radarRef = ref(null)
let radarChart = null

const totalMatches = computed(() =>
    props.whiteMatches.length + props.blackMatches.length
)

const monthlyData = computed(() => 
{
    const all = [
        ...props.whiteMatches,
        ...props.blackMatches,
    ]

    const order = locale.value === 'hu'
        ? ['Jan','Feb','Már','Ápr','Máj','Jún','Júl','Aug','Sze','Okt','Nov','Dec']
        : ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec']

    const counts = {}
    order.forEach(m => (counts[m] = 0))

    all.forEach(x => 
    {
        const date = new Date(x.played_at)
        if (!isNaN(date)) 
        {
            const label = date.toLocaleString(locale.value === 'hu' 
                ? 'hu-HU' 
                : 'en-US', { month: 'short' })
                .replace('.', '').trim()
                .replace(/^./, c => c.toUpperCase())

            if (counts[label] !== undefined)
            {
                counts[label]++
            }
        }
    })

    const filled = order.filter(x => counts[x] > 0)
    const labels = filled.length >= 2 
        ? filled 
        : order.slice(0, 6)

    return { 
      labels, data: labels.map(x => counts[x]) 
    }
})

const won = computed(() => {
    return allMatches.value.filter(m => String(m.winner_id) === String(props.myId)).length
})

const lost = computed(() =>
    allMatches.value.filter(x =>
        x.winner_id !== 'Unknown' && String(x.winner_id) !== String(props.myId)
    ).length
)

const draw = computed(() =>
    allMatches.value.filter(x => x.winner_id === 'Unknown').length
)

const getChartBorderColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartBorderColor')
        .trim()
}

const getChartBackgroundColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartBackground')
        .trim()
}

const getChartGridColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartGridColor')
        .trim()
}

const getChartTickColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartGridTickColor')
        .trim()
}

const getChartPointBackgroundColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartPointBackgroundColor')
        .trim()
}

const getChartPointWinColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartPointWinBackgroundColor')
        .trim()
}

const getChartPointLoseColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartPointLoseBackgroundColor')
        .trim()
}

const getChartPointDrawColor = () => 
{
    return getComputedStyle(document.documentElement)
        .getPropertyValue('--ChartPointDrawBackgroundColor')
        .trim()
}

function buildCharts() 
{
    if (radarChart)
    {
        radarChart.destroy()
    }

    if (lineChart)
    {
        lineChart.destroy()
    }

    radarChart = new Chart(radarRef.value, 
    {
        type: 'radar',
        data: {
            labels: [t('match.won'), t('match.lost'), t('match.drawn')],
            datasets: [{
                data: [won.value, lost.value, draw.value],
                borderColor: getChartBorderColor(),
                backgroundColor: getChartBackgroundColor(),
                borderWidth: 1,
                pointRadius: 4,
                pointBackgroundColor: [getChartPointWinColor(), getChartPointLoseColor(), getChartPointDrawColor()],
            }],
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: { legend: { display: false } },
            scales: {
                r: {
                    ticks: { display: false },
                    grid: { color: getChartGridColor() },
                    pointLabels: { font: { size: 12 }, color: [getChartPointWinColor(), getChartPointLoseColor(), getChartPointDrawColor()] },
                    beginAtZero: true,
                    suggestedMax: props.whiteMatches.length + props.blackMatches.length,
                },
            },
            pointLabels: {
                font: { size: 12, weight: '800' }
            },
        },
    })

    lineChart = new Chart(lineRef.value, {
        type: 'line',
        data: {
            labels: monthlyData.value.labels,
            datasets: [{
                label: t('match.matches'),
                data: monthlyData.value.data,
                borderColor: getChartBorderColor(),
                backgroundColor: getChartBackgroundColor(),
                borderWidth: 1,
                pointRadius: 4,
                pointBackgroundColor: getChartPointBackgroundColor(),
                tension: 0.4,
                fill: true,
            }],
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: { legend: { display: false } },
            scales: {
                x: {
                    ticks: { font: { size: 10 }, color: getChartTickColor() },
                    grid: { display: false },
                    border: { display: false },
                },
                y: {
                    ticks: { font: { size: 10 }, color: getChartTickColor(), stepSize: 1 },
                    grid: { color: getChartGridColor() },
                    border: { display: false },
                    beginAtZero: true,
                },
            },
        },
    })
}

onMounted(buildCharts)

watch(
    () => [props.whiteMatches, props.blackMatches, props.myId, locale.value],
    () => {
        requestAnimationFrame(() => {
            buildCharts()
        })
    },
    { deep: true }
)

let themeObserver = null

onMounted(() => 
{
    buildCharts()

    themeObserver = new MutationObserver(() => 
    {
        requestAnimationFrame(() => buildCharts())
    })

    themeObserver.observe(document.documentElement, 
    {
        attributes: true,
        attributeFilter: ['class', 'data-theme'],
    })
})

onUnmounted(() => 
{
    themeObserver?.disconnect()
    lineChart?.destroy()
    radarChart?.destroy()
})

const chartConfig = computed(() => 
({
    matches: { label: t('match.matches'), color: undefined },
    white: { label: t('match.white'), color: "#e8e8e8" },
    black: { label: t('match.black'), color: "#1a1a1a" },
}))

const chartData = computed(() => 
[
    { side: "white", white: props.whiteMatches.length, fill: "#e8e8e8" },
    { side: "black", black: props.blackMatches.length, fill: "#1a1a1a" },
])
</script>
