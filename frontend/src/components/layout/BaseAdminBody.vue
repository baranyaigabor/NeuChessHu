<script setup>
import BaseNavBar from '@components/layout/BaseNavBar.vue'
import { computed, onMounted, ref } from 'vue'
import { api } from '@utils/http.mjs'
import { useUserStore, useUsersStore } from '@stores/UserStore.mjs'
import Infos from '@components/ui/publicInfos/infos.vue'

const users = ref([])
const userStore = useUserStore()
const usersStore = useUsersStore()

onMounted(async () => {
  users.value = await usersStore.getUsers()
})

const visibleUsers = computed(() =>
  users.value.filter((user) => user.nickname?.toLowerCase() !== 'stockfish')
)

async function handleSave(user, updatedUser) {
  try {
    const payload = normalizeUserPayload(updatedUser)
    const savedUser = typeof usersStore.updateUser === 'function'
      ? await usersStore.updateUser(user.nickname, payload)
      : await updateUserFallback(user.nickname, payload)

    users.value = users.value.map((item) =>
      item.id === user.id ? savedUser : item
    )
  } catch (error) {
    console.error('Admin user update failed:', error.response?.data ?? error)
  }
}

async function updateUserFallback(identifier, updatedUser) {
  const response = await api.patch(`users/${identifier}`, normalizeUserPayload(updatedUser), {
    headers: {
      Authorization: `Bearer ${userStore.token}`
    }
  })

  return response.data.data
}

function normalizeUserPayload(data) {
  const payload = {}

  if (data.nickname && data.nickname !== 'Unknown') {
    payload.nickname = data.nickname
  }

  if (data.full_name) {
    const parts = data.full_name.trim().split(' ')
    payload.first_name = parts[0] || ''
    payload.last_name = parts.slice(1).join(' ') || ''
  } else {
    if (data.first_name !== undefined) {
      payload.first_name = data.first_name === 'Unknown' ? null : data.first_name
    }

    if (data.last_name !== undefined) {
      payload.last_name = data.last_name === 'Unknown' ? null : data.last_name
    }
  }

  if (data.region !== undefined) {
    payload.region = data.region === 'Unknown' || data.region === '' ? null : data.region
  }

  if (data.date_of_birth !== undefined) {
    payload.date_of_birth = data.date_of_birth === 'Unknown' || data.date_of_birth === '' ? null : data.date_of_birth
  }

  if (isValidImageDataUrl(data.profile_picture)) {
    payload.profile_picture = data.profile_picture
  }

  return payload
}

function isValidImageDataUrl(value) {
  if (typeof value !== 'string') return false

  const match = value.match(/^data:image\/(jpe?g|png);base64,([A-Za-z0-9+/]+={0,2})$/)
  if (!match) return false

  try {
    const bytes = atob(match[2])
    const isPng = bytes.charCodeAt(0) === 0x89 &&
                  bytes.slice(1, 4) === 'PNG'
    const isJpeg = bytes.charCodeAt(0) === 0xff &&
                   bytes.charCodeAt(1) === 0xd8 &&
                   bytes.charCodeAt(2) === 0xff

    return isPng || isJpeg
  } catch {
    return false
  }
}
</script>

<template>
  <div class="flex min-h-screen flex-col bg-(--WindowBrush) text-(--TextBrush)">
    <BaseNavBar />

    <div class="mx-auto flex w-full max-w-[1600px] flex-1 flex-col gap-4 px-3 pb-6 sm:px-4 md:px-6 lg:flex-row lg:gap-5 lg:px-6 lg:pb-8">
      <main class="w-full min-w-0 flex-1">
        <div v-for="user in visibleUsers" :key="user.id" class="bg-(--SideBarBrush) border border-black! rounded shadow flex flex-col items-center my-5 md:mx-6">
            <Infos
              :user="user"
              :userId="user.id"
              :isOwner="true"
              @save="(updatedUser) => handleSave(user, updatedUser)"
            />
        </div>
      </main>
    </div>
  </div>
</template>
