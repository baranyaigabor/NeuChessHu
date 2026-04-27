<script setup>
import { Infos, MatchInfos, PersonalStat, ChessStat } from "@components/ui/publicInfos";
import { useUserStore } from "@stores/UserStore";
import { useRouter, useRoute } from "vue-router";
import { computed, ref, watch } from "vue";
import { useI18n } from '@utils/i18n'

const userStore = useUserStore();
const router = useRouter();
const route = useRoute();
const { t } = useI18n()

const profileUser = ref(null);

async function loadProfile() {
  try {
    profileUser.value = null;
    profileUser.value = await userStore.fetchUser(route.params.nickname);
  } catch (e) {
    console.error(t('profile.loadFailed'), e);
  }
}

watch(() => route.params.nickname, loadProfile, { immediate: true });

const isOwner = computed(() => {
  return !!userStore.token && userStore.user?.data?.nickname === route.params.nickname;
});

async function handleSave(updatedUser) {
  await userStore.updateUser(updatedUser);
  profileUser.value = await userStore.fetchUser(route.params.nickname);
  router.push({ name: "user", params: { nickname: userStore.user.data.nickname } });
}
</script>

<template>
  <div
    v-if="profileUser"
    class="mx-auto flex w-full max-w-[1680px] flex-col gap-4 px-3 sm:px-4 md:gap-6 2xl:flex-row 2xl:items-stretch"
  >
    <div class="w-full 2xl:w-[34%] 2xl:flex">
      <div class="profile-eq-card bg-[var(--SideBarBrush)] border !border-black w-full p-4 rounded shadow flex items-center justify-center">
        <PersonalStat
          :white-matches="profileUser.white_matches"
          :black-matches="profileUser.black_matches"
          :myId="userStore.user.data.id"
        />
      </div>
    </div>

    <div class="w-full 2xl:w-[66%] flex flex-col gap-4">
      <div class="profile-eq-card bg-[var(--SideBarBrush)] border !border-black rounded shadow flex flex-col items-center gap-2">
        <Infos
          :user="profileUser"
          :user-id="userStore.user.data.id"
          :is-owner="isOwner"
          @save="handleSave"
        />
      </div>

      <div class="profile-eq-card bg-[var(--SideBarBrush)] border !border-black p-4 rounded shadow flex flex-col gap-2">
        <MatchInfos
          :white-matches="profileUser.white_matches"
          :black-matches="profileUser.black_matches"
          :my-id="profileUser.id"
        />
      </div>

      <div class="profile-eq-card bg-[var(--SideBarBrush)] border !border-black p-4 rounded shadow flex flex-col gap-2">
        <ChessStat
          :white-matches="profileUser.white_matches"
          :black-matches="profileUser.black_matches"
          :my-id="profileUser.id"
        />
      </div>
    </div>
  </div>

  <div v-else class="flex items-center justify-center p-12 text-gray-500">
    {{ t('profile.loading') }}
  </div>
</template>

<style scoped>
.profile-eq-card {
  height: 39rem;
}

.profile-eq-card > * {
  height: 100%;
  min-height: 0;
}

@media (min-width: 865px) {
  .profile-eq-card {
    height: auto;
  }
}
</style>