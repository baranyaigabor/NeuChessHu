<script setup>
import BaseLoginHeader from '@components/layout/BaseLoginHeader.vue'
</script>

<template>
  <BaseLoginHeader />

  <main class="container mx-auto min-h-[calc(100vh-7rem)] bg-(--WindowBrush) text-(--TextBrush)">
    <slot />
  </main>
</template>
