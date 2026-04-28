<script setup>
import { PersonalInfos, MatchInfos, PersonalStat, ChessStat } from "@components/ui/public-infos";
import { useUserStore } from "@stores/UserStore";
import { useRouter, useRoute } from "vue-router";
import { computed, ref, watch } from "vue";
import { useI18n } from '@utils/i18n'

const userStore = useUserStore();
const router = useRouter();
const route = useRoute();
const { t } = useI18n()

const profileUser = ref(null);
const authenticatedUser = computed(() => userStore.user?.data ?? userStore.user ?? null);
const authenticatedUserId = computed(() => userStore.userId ?? authenticatedUser.value?.id ?? null);
const authenticatedUserRole = computed(() => authenticatedUser.value?.role ?? null);
const authenticatedUserNickname = computed(() => authenticatedUser.value?.nickname ?? null);

async function loadProfile() 
{
    try 
    {
        profileUser.value = null;
        profileUser.value = await userStore.fetchUser(route.params.nickname);
        document.title = profileUser.value?.nickname || 'NeuChess';
    } 

    catch (e) 
    {
        console.error(t('profile.loadFailed'), e);
    }
}

watch(() => route.params.nickname, loadProfile, { immediate: true });

const isOwner = computed(() => 
{
    if (!userStore.token || !profileUser.value)
    {
        return false
    }

    if (authenticatedUserId.value !== null && profileUser.value.id !== undefined)
    {
        return String(authenticatedUserId.value) === String(profileUser.value.id)
    }

    return !!authenticatedUserNickname.value && authenticatedUserNickname.value === profileUser.value.nickname
});

async function handleSave(updatedUser) 
{
    try
    {
        await userStore.updateUser(updatedUser);
        profileUser.value = await userStore.fetchUser(route.params.nickname);
        router.push({ name: "user", params: { nickname: authenticatedUser.value.nickname } });
    }

    catch (error)
    {
        console.error('Profile update failed:', error.response?.data ?? error)

        if (error.response?.status === 401 || error.message === 'Unauthenticated')
        {
            await userStore.logout()
            router.push({ name: "signin" })
        }
    }
}

async function handleDelete(userId)
{
    try
    {
        await userStore.deleteUser(userId ?? profileUser.value?.id);

        if (authenticatedUserRole.value !== 'admin')
        {
            router.push({ name: "signin" });
        }
    }

    catch (error)
    {
        console.error('Profile delete failed:', error.response?.data ?? error)

        if (error.response?.status === 401)
        {
            await userStore.logout()
            router.push({ name: "signin" })
        }
    }
}
</script>

<template>
    <div v-if="profileUser" class="mx-auto flex w-full max-w-[1680px] flex-col gap-4 px-3 sm:px-4 md:gap-6 2xl:flex-row 2xl:items-stretch">
        <div class="contents 2xl:flex 2xl:w-[34%]">
            <div class="order-3 bg-(--SideBarBrush) border border-black! w-full p-4 rounded shadow flex items-center justify-center 2xl:order-none 2xl:h-full">
                <PersonalStat :white-matches="profileUser.white_matches" :black-matches="profileUser.black_matches" :myId="profileUser.id" />
            </div>
        </div>

        <div class="contents 2xl:flex 2xl:w-[66%] 2xl:flex-col 2xl:gap-4 2xl:self-stretch">
            <div class="order-1 bg-(--SideBarBrush) border border-black! rounded shadow flex flex-col items-center gap-2 2xl:order-none 2xl:shrink-0">
                <PersonalInfos :user="profileUser" :user-id="authenticatedUserId" :is-owner="isOwner" @save="handleSave" @delete="handleDelete" />
            </div>

            <div class="order-2 bg-(--SideBarBrush) border border-black! p-4 rounded shadow flex flex-col gap-2 2xl:order-none 2xl:min-h-0 2xl:flex-1 2xl:justify-center">
                <MatchInfos :white-matches="profileUser.white_matches" :black-matches="profileUser.black_matches" :my-id="profileUser.id" />
            </div>

            <div class="order-4 bg-(--SideBarBrush) border border-black! p-4 rounded shadow flex flex-col gap-2 2xl:order-none 2xl:shrink-0">
                <ChessStat :white-matches="profileUser.white_matches" :black-matches="profileUser.black_matches" :my-id="profileUser.id" />
            </div>
        </div>
    </div>
</template>