<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'

import BaseLoginHeader from '@components/layout/BaseLoginHeader.vue'
import BaseSidebar from '@components/layout/BaseSidebar.vue'
import BaseWelcomeBody from '@components/layout/BaseWelcomeBody.vue'
import BaseUserBody from '@components/layout/BaseUserBody.vue'

const route = useRoute()
const showWelcomeBody = computed(() => route.name === 'welcome')
</script>

<template>
  <BaseLoginHeader />
  
  <div class="flex">
    <BaseSidebar class="mr-5" />

    <main>
      <BaseWelcomeBody v-if="showWelcomeBody"/>
      <BaseUserBody v-else/>
    </main>
  </div>
</template>