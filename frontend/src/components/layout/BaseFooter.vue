<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { termsUrl, userGuideUrl } from '@utils/docs'

const { locale, t } = useI18n()
const currentYear = computed(() => new Date().getFullYear())
const termsHref = computed(() => termsUrl(locale.value))
const userGuideHref = computed(() => userGuideUrl(locale.value))

const contacts = [
    {
        email: 'authsupport@neuchess.hu',
        labelKey: 'footer.supportLabel'
    },
    {
        email: 'observer@neuchess.hu',
        labelKey: 'footer.observerLabel'
    },
    {
        email: 'team@neuchess.hu',
        labelKey: 'footer.teamLabel'
    }
]
</script>

<template>
    <footer class="mt-auto border-t border-(--BorderBrush) bg-(--FooterBrush) px-4 py-4 text-center text-[12px] text-(--FooterTextBrush)!">
        <div class="mx-auto flex max-w-[1200px] flex-col items-center gap-2">
            <div class="flex w-full flex-wrap items-center justify-center gap-x-3 gap-y-2 md:flex-nowrap lg:gap-x-5">
                <span v-for="contact in contacts" :key="contact.email" class="whitespace-nowrap text-center text-(--FooterTextBrush)!">
                    
                    <span class="font-semibold">{{ t(contact.labelKey) }}: </span>

                    <a :href="`mailto:${contact.email}`" class="text-(--FooterTextBrush)! no-underline hover:underline">
                        {{ contact.email }}
                    </a>

                </span>
            </div>

            <div class="flex w-full flex-wrap items-center justify-center gap-x-3 gap-y-2 md:flex-nowrap lg:gap-x-5">
                <a :href="termsHref" class="font-semibold text-(--FooterTextBrush)! no-underline hover:underline sm:text-left">
                    {{ t('footer.terms') }}
                </a>

                <p class="m-0 opacity-85">
                    {{ t('footer.copyright', { year: currentYear }) }}
                </p>

                <a :href="userGuideHref" class="font-semibold text-(--FooterTextBrush)! no-underline hover:underline sm:text-right">
                    {{ t('footer.userGuide') }}
                </a>
            </div>
        </div>
    </footer>
</template>