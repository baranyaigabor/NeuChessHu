<script setup>
import { onMounted, onBeforeUnmount } from 'vue'
import { useUserStore } from '@stores/UserStore.mjs'

onMounted(() => {
    window.addEventListener('beforeunload', handleUnload)
})

onBeforeUnmount(() => {
    window.removeEventListener('beforeunload', handleUnload)
})

function handleUnload()
{
    const token = useUserStore().token

    if(token)
    {
        const logoutUrl = new URL('logout', import.meta.env.VITE_BACKEND_URL)
        logoutUrl.searchParams.set('token', token)

        navigator.sendBeacon(logoutUrl.toString())
    }
}
</script>

<template>
    <div class="min-h-screen bg-(--WindowBrush) text-(--TextBrush)">
        <RouterView />
    </div>
</template>