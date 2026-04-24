<script setup>
import { onMounted, onBeforeUnmount } from 'vue'
import { useUserStore } from '@stores/UserStore'

onMounted(() => {
    window.addEventListener('beforeunload', handleUnload)
})

onBeforeUnmount(() => {
    window.removeEventListener('beforeunload', handleUnload)
})

function handleUnload()
{
    const token = useUserStore().token

    if(token !== null)
    {
        navigator.sendBeacon(`/api/logout?token=${token}`)
    }
}
</script>

<template>
    <div class="min-h-screen bg-(--WindowBrush) text-(--TextBrush)">
        <RouterView />
    </div>
</template>
