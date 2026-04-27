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