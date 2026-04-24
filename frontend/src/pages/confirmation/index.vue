<script setup>
import BaseLoginLayout from '@layouts/BaseLoginLayout.vue'
import BaseConfirmationsCard from '@components/layout/BaseConfirmationsCard.vue'
</script>

<template>
    <BaseLoginLayout>
        <BaseConfirmationsCard />
    </BaseLoginLayout>
</template>

<route lang="yaml">
name: confirminformation
meta:
    title: Confirmations
</route>