<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'

const { locale, t } = useI18n()
const currentYear = computed(() => new Date().getFullYear())
const termsHref = computed(() =>
  locale.value === 'hu'
    ? 'http://docs.vm2.test/hu/terms'
    : 'http://docs.vm2.test/en/terms'
)

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
  <footer class="mt-auto border-t border-(--BorderBrush) bg-(--FooterBrush) px-4 py-4 text-center text-[12px] text-(--FooterTextBrush)">
    <div class="mx-auto flex max-w-[900px] flex-col items-center gap-2">
      <a
        :href="termsHref"
        class="footer-link font-semibold text-(--FooterTextBrush) no-underline hover:underline">
        {{ t('footer.terms') }}
      </a>

      <div class="flex flex-wrap items-center justify-center gap-x-5 gap-y-2">
        <a
          v-for="contact in contacts"
          :key="contact.email"
          :href="`mailto:${contact.email}`"
          class="footer-link text-(--FooterTextBrush) no-underline hover:underline">
          <span class="font-semibold">{{ t(contact.labelKey) }}:</span>
          {{ contact.email }}
        </a>
      </div>

      <p class="m-0 opacity-85">
        {{ t('footer.copyright', { year: currentYear }) }}
      </p>
    </div>
  </footer>
</template>

<style lang="css">
.footer-link {
  overflow-wrap: anywhere;
}
</style>
