<script setup>
import { computed, onMounted, onUnmounted, ref } from 'vue'
import { ChevronLeft, ChevronRight } from 'lucide-vue-next'
import { useI18n } from '@utils/i18n'
import webExampleLight from '@/assets/examples/web_example_light.png'
import webExampleDark from '@/assets/examples/web_example_dark.png'
import appExampleLight from '@/assets/examples/app_example_light.png'
import appExampleDark from '@/assets/examples/app_example_dark.png'

const { t } = useI18n()
const activeExampleIndex = ref(0)
let carouselTimer = null

const exampleSlides = [
    {
        src: webExampleLight,
        alt: 'Web light example',
        frameClass: 'w-full',
        viewportClass: 'aspect-[1913/990]',
        imageClass: 'h-full w-full object-contain object-center',
    },
    {
        src: webExampleDark,
        alt: 'Web dark example',
        frameClass: 'w-full',
        viewportClass: 'aspect-[1913/990]',
        imageClass: 'h-full w-full scale-[1.01] object-contain object-center',
    },
    {
        src: appExampleLight,
        alt: 'App light example',
        frameClass: 'mx-auto w-[86%]',
        viewportClass: 'aspect-[3024/1754]',
        imageClass: 'h-full w-full object-contain object-center',
    },
    {
        src: appExampleDark,
        alt: 'App dark example',
        frameClass: 'mx-auto w-[86%]',
        viewportClass: 'aspect-[3024/1754]',
        imageClass: 'h-full w-full object-contain object-center',
    },
]

const activeExample = computed(() => exampleSlides[activeExampleIndex.value])

function showPreviousExample()
{
    activeExampleIndex.value = (activeExampleIndex.value - 1 + exampleSlides.length) % exampleSlides.length
}

function showNextExample()
{
    activeExampleIndex.value = (activeExampleIndex.value + 1) % exampleSlides.length
}

function showExample(index)
{
    activeExampleIndex.value = index
}

onMounted(() =>
{
    carouselTimer = window.setInterval(showNextExample, 5000)
})

onUnmounted(() =>
{
    window.clearInterval(carouselTimer)
})
</script>

<template>
    <div class="mx-auto flex w-full max-w-7xl -my-5 flex-col gap-8 px-3 pb-10 sm:px-4 lg:min-h-[calc(100vh-14rem)] lg:flex-row lg:items-center lg:gap-10 lg:pb-0">
        <div class="w-full lg:w-[68%]">
         <div class="relative aspect-[1913/990] w-full">
                <div class="absolute inset-0 flex items-center justify-center">
                    <div class="relative overflow-hidden rounded border border-(--BorderBrush)! bg-(--SideBarBrush) shadow" :class="activeExample.frameClass">
                        <div class="flex w-full items-center justify-center bg-(--WindowBrush)" :class="activeExample.viewportClass">
                            <img :src="activeExample.src" :alt="activeExample.alt" :class="activeExample.imageClass" />
                        </div>
                    </div>
                </div>

                <button type="button" class="absolute left-3 top-1/2 flex h-9 w-9 -translate-y-1/2 items-center justify-center rounded border border-(--BorderBrush)! bg-(--ButtonBrush)! text-(--TextBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] transition hover:bg-(--ButtonHoverBrush)!" :aria-label="activeExample.alt" @click="showPreviousExample">
                    <ChevronLeft class="h-5 w-5" />
                </button>

                <button type="button" class="absolute right-3 top-1/2 flex h-9 w-9 -translate-y-1/2 items-center justify-center rounded border border-(--BorderBrush)! bg-(--ButtonBrush)! text-(--TextBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] transition hover:bg-(--ButtonHoverBrush)!" :aria-label="activeExample.alt" @click="showNextExample">
                    <ChevronRight class="h-5 w-5" />
                </button>
            </div>

            <div class="mt-3 flex items-center justify-center gap-2">
                <button v-for="(slide, index) in exampleSlides" :key="slide.alt" type="button" class="block h-3 appearance-none hover:shadow-[inset_0_0_0_1px_var(--BorderBrush)]! overflow-hidden rounded-[999px]! border border-(--BorderBrush)! p-0 leading-none transition" :class="activeExampleIndex === index ? 'w-8 bg-(--CarouselStepper)!' : 'w-3 bg-(--ButtonBrush)! hover:bg-(--ButtonHoverBrush)!'" :aria-label="slide.alt" @click="showExample(index)"></button>
            </div>
        </div>

        <div class="flex w-full flex-col lg:w-[32%] lg:self-center">
            <div class="flex justify-center lg:justify-start">
                <h1>{{ t('welcome.title') }}</h1>
            </div>

            <h2 class="text-center lg:text-left!">{{ t('welcome.subtitle') }}</h2>
            <h3 class="text-center lg:text-left!">{{ t('welcome.community') }}</h3>
            <h4 class="text-center lg:text-left!">{{ t('welcome.fun') }}</h4>

            <div class="mt-6 flex w-full justify-center lg:justify-start">
                <RouterLink
                    class="flex items-center w-fit rounded bg-[var(--ButtonBrush)] p-3.5 text-[var(--TextBrush)] border !border-[var(--BorderBrush)] !no-underline"
                    :to="{ name: 'signin' }">
                    <svg class="w-6 h-6 stroke-current text-[var(--LogoBrush)]" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" fill="currentColor">
                        <path d="M0 0 C2.53371888 2.22077443 4.96997564 4.50426605 7.375 6.86328125 C7.94089844 7.4046875 8.50679687 7.94609375 9.08984375 8.50390625 C21.68999859 21.73908216 23.97470077 39.72885944 23.6015625 57.15234375 C23.42029513 62.5519141 23.07283726 67.92253086 22.5625 73.30078125 C22.49264893 74.04336182 22.42279785 74.78594238 22.35083008 75.55102539 C20.96545428 88.23169402 17.79742709 100.55751361 14.3125 112.796875 C13.9200309 114.19131834 13.52808712 115.58590962 13.1366272 116.9806366 C12.31269595 119.91263358 11.48548981 122.84367696 10.65557861 125.77398682 C9.35889043 130.35370426 8.07193321 134.93612178 6.78662109 139.51904297 C3.6988184 150.52418785 0.60425073 161.52741921 -2.51074219 172.52490234 C-18.27271387 219.42057474 -18.27271387 219.42057474 -12.9375 266.48828125 C-5.27728132 274.35553287 6.03318357 275.26112788 16.4453125 275.828125 C31.16557634 275.95981722 45.45112027 271.18787448 59.375 266.86328125 C58.96387957 269.03100714 58.54564941 271.19738429 58.125 273.36328125 C57.77695312 275.173125 57.77695312 275.173125 57.421875 277.01953125 C57.25373291 277.72859619 57.08559082 278.43766113 56.91235352 279.16821289 C56.29026204 282.03351031 56.29026204 282.03351031 56.03881836 285.68139648 C55.43316847 290.74342678 54.80761707 295.86116644 50.67401123 299.26324463 C45.2932983 302.38658677 39.35243149 304.10484946 33.4375 305.92578125 C31.59752918 306.54281407 29.75946493 307.16556102 27.92333984 307.79394531 C24.34336125 309.00835037 20.75501256 310.18679582 17.15185547 311.33056641 C13.29974551 312.55542739 9.48910896 313.86396374 5.69140625 315.25 C-10.82571372 321.25001174 -26.23148505 323.12074557 -43.75 323.23828125 C-44.46489594 323.24640839 -45.17979187 323.25453552 -45.91635132 323.26290894 C-61.24556633 323.39331139 -75.51728589 321.24099139 -89.625 314.86328125 C-90.55828125 314.44433594 -91.4915625 314.02539062 -92.453125 313.59375 C-107.45735332 306.15593968 -118.69050165 293.0996443 -124.125 277.30078125 C-135.17046517 240.4314276 -119.00093482 198.03253012 -108.81335449 162.60351562 C-106.95320686 156.13091299 -105.10294721 149.65547038 -103.25390625 143.1796875 C-102.95535862 142.13440069 -102.95535862 142.13440069 -102.65077972 141.06799698 C-99.94227232 131.57723432 -97.2823679 122.07483298 -94.70703125 112.546875 C-94.2975399 111.03765221 -94.2975399 111.03765221 -93.879776 109.49794006 C-83.75818449 74.82632566 -83.75818449 74.82632566 -90.30859375 40.546875 C-94.89417238 34.68223289 -101.57603667 32.96292765 -108.625 31.86328125 C-112.33809401 31.55222594 -116.02488982 31.48204623 -119.75 31.48828125 C-121.26460815 31.49069824 -121.26460815 31.49069824 -122.80981445 31.49316406 C-131.31187237 31.63148608 -138.57123591 33.04588675 -146.625 35.86328125 C-148.87466213 36.63510009 -151.12464236 37.40599264 -153.375 38.17578125 C-154.31859375 38.52253906 -155.2621875 38.86929688 -156.234375 39.2265625 C-158.625 39.86328125 -158.625 39.86328125 -161.625 38.86328125 C-160.91534314 34.8095772 -160.13639255 30.77596048 -159.29345703 26.74780273 C-159.01703826 25.37909439 -158.75915166 24.00649742 -158.52099609 22.63061523 C-156.53659443 11.28000241 -156.53659443 11.28000241 -152.07495117 7.97338867 C-148.95867611 6.66508282 -145.88812007 5.7366676 -142.625 4.86328125 C-140.76903882 4.15954523 -138.9209012 3.43475738 -137.08203125 2.6875 C-95.769872 -13.068647 -39.01607338 -31.75861925 0 0 Z " transform="translate(303.625,189.13671875)"/>
                        <path d="M0 0 C4.38309323 3.29103441 8.3941233 6.89471534 12.3046875 10.73046875 C12.87058594 11.271875 13.43648438 11.81328125 14.01953125 12.37109375 C24.28657459 23.15557429 28.1540884 38.25789515 28.3046875 52.73046875 C27.32027697 69.52013726 20.66974318 83.05230423 8.4296875 94.54296875 C-6.61659583 107.40696671 -24.11470608 112.00659789 -43.6953125 110.73046875 C-61.27379045 108.3214268 -77.27412964 100.62717892 -88.6953125 86.73046875 C-99.47945392 72.0915704 -102.55859331 56.26064018 -100.26953125 38.41015625 C-96.64081806 21.71362315 -86.1629758 8.81239956 -72.0390625 -0.29296875 C-50.47155014 -13.0419889 -21.30363095 -13.95136775 0 0 Z " transform="translate(341.6953125,9.26953125)"/>
                    </svg>
                    <span class="ml-2 text-(--TextBrush) text-sm font-medium">{{ t('welcome.getStarted') }}</span>
                </RouterLink>
            </div>
        </div>
    </div>
</template>
