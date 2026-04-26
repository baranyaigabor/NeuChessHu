<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { countryName, countryValueFromStoredName, countryValues } from '@utils/i18n/countries'

const props = defineProps({
    value: {
        type: String,
        default: ""
    }
})

const emit = defineEmits(['regionChange'])
const { locale, t } = useI18n()

const localizedCountries = computed(() =>
    countryValues.map((value) => ({
        value,
        name: countryName(value, 'hu'),
        label: countryName(value, locale.value),
    }))
)

const selectedCountryValue = computed(() =>
    countryValueFromStoredName(props.value, localizedCountries.value)
)

function onRegionChange(event) 
{
    emit('regionChange', event.target.value)
}
</script>

<template>
    <div class="w-full min-w-0">
        <select :value="selectedCountryValue" class="m-0 max-h-16 w-full min-w-0 appearance-none rounded-[5px] border border-black! bg-(--ButtonBrush)! bg-[url(&quot;data:image/svg+xml,%3Csvg_xmlns='http://www.w3.org/2000/svg'_width='12'_height='12'_viewBox='0_0_12_12'%3E%3Cpath_fill='%23333'_d='M6_8L1_3h10z'/%3E%3C/svg%3E&quot;)] bg-[right_10px_center] bg-no-repeat p-1 px-1.5! pr-[30px] text-(--FieldTextBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] focus:bg-(--ButtonBrush)! focus:outline-none! [&_option]:bg-(--ButtonBrush) [&_option]:text-(--FieldTextBrush)"
            @change="onRegionChange" id="region" name="region" required>

            <option value="" disabled>{{ t('common.chooseCountry') }}</option>
            <option v-for="c in localizedCountries" :key="c.value" :value="c.value">
                {{ c.label }}
            </option>

        </select>
    </div>
</template>

<style lang="css">
select::-webkit-scrollbar {
    width: 12px;
}

select::-webkit-scrollbar-track {
    border-radius: 6px;
}

select::-webkit-scrollbar-thumb {
    background-color: var(--SelectIconBrush);
    border-radius: 6px;
    border: 2px solid var(--ButtonBrush);
}

select::-webkit-scrollbar-thumb:hover {
    background-color: var(--SelectIconHoverBrush);
}
</style>
