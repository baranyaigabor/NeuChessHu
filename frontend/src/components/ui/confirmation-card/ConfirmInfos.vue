<script setup>
import { computed } from "vue"
import { useUserStore } from "@stores/UserStore.mjs"
import { useI18n } from '@utils/i18n'
import { countryName } from '@utils/i18n/countries'

const userStore = useUserStore()
const { locale, t } = useI18n()

const formattedData = computed(() => {
  const raw = userStore.getRegistrationData
  const format = (x) => (x === null || x === "" ? t('common.unknown') : x)

  return {
    nickname: format(raw.nickname),
    email: format(raw.email),
    password: format(raw.password),
    first_name: format(raw.first_name),
    last_name: format(raw.last_name),
    region: raw.region ? countryName(raw.region, locale.value) : t('common.unknown'),
    date_of_birth: format(raw.date_of_birth),
    profile_picture: raw.profile_picture ? raw.profile_picture : t('common.unknown')
  }
})
</script>

<template>
  <div class="flex flex-col gap-4">
    <h2 class="text-lg text-left font-semibold text-(--TextBrush)!">{{ t('registration.confirmInformation') }}</h2>

    <div class="flex flex-col gap-2">
      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.nickname') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.nickname }}</span>
      </div>

      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.email') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.email }}</span>
      </div>

      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.password') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.password }}</span>
      </div>

      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.firstName') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.first_name }}</span>
      </div>

      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.lastName') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.last_name }}</span>
      </div>

      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.region') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.region }}</span>
      </div>

      <div class="flex justify-between border-b border-(--BorderChangingBrush)/24 pb-1">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.dateOfBirth') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.date_of_birth }}</span>
      </div>

      <div class="flex justify-between pb-3">
        <span class="text-xs font-semibold text-(--TextBrush)">{{ t('common.profilePicture') }}:</span>
        <span class="text-xs text-(--TextBrush)">{{ formattedData.profile_picture }}</span>
      </div>
    </div>
  </div>
</template>