<script setup>
import BaseFooter from '@components/layout/BaseFooter.vue'
import BaseLoginHeader from '@components/layout/BaseLoginHeader.vue'
</script>

<template>
  <div class="flex min-h-screen flex-col bg-(--WindowBrush) text-(--TextBrush)">
    <BaseLoginHeader />

    <main class="container mx-auto flex-1 bg-(--WindowBrush) text-(--TextBrush)">
      <slot />
    </main>

    <BaseFooter />
  </div>
</template>