<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'

import BaseLoginHeader from '@components/layout/BaseLoginHeader.vue'
import BaseFooter from '@components/layout/BaseFooter.vue'
import BaseSidebar from '@components/layout/BaseSidebar.vue'
import BaseWelcomeBody from '@components/layout/BaseWelcomeBody.vue'
import BaseUserBody from '@components/layout/BaseUserBody.vue'
import BaseMatchBody from '@components/layout/BaseMatchBody.vue'

const route = useRoute()
const showWelcomeBody = computed(() => route.name === 'welcome')
const showUserBody = computed(() => route.name === 'user')
const showMatchBody = computed(() => route.name === 'matches')
</script>

<template>
  <div class="flex min-h-screen flex-col bg-(--WindowBrush) text-(--TextBrush)">
    <BaseLoginHeader />

    <div class="mx-auto flex w-full max-w-[1600px] flex-1 flex-col gap-4 px-3 pb-6 sm:px-4 md:px-6 lg:flex-row lg:gap-5 lg:px-6 lg:pb-8">
      <BaseSidebar class="w-full lg:w-auto lg:shrink-0" />

      <main class="w-full min-w-0 flex-1">
        <BaseWelcomeBody v-if="showWelcomeBody" />
        <BaseUserBody v-else-if="showUserBody" />
        <BaseMatchBody v-else-if="showMatchBody" />
      </main>
    </div>

    <BaseFooter />
  </div>
</template>