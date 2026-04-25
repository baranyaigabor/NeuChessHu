<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { countryName, countryValues } from '@utils/i18n/countries'

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
    label: countryName(value, locale.value),
  }))
)

function onRegionChange(event) 
{
  emit('regionChange', event.target.value)
}
</script>

<template>
    <div class="col">
        <label class="m-0 mb-1" for="region">{{ t('common.region') }}:</label>
        <select :value="props.value" class="custom-select m-0 max-h-16! w-full rounded-[5px] border border-[var(--BorderBrush)] !bg-[var(--ButtonBrush)] p-1.5 ps-2 text-[var(--FieldTextBrush)] shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] focus:!bg-[var(--ButtonBrush)] focus:!outline-none"
            @change="onRegionChange" id="region" name="region">

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

.custom-select {
  appearance: none;
  -webkit-appearance: none;
  color: var(--FieldTextBrush);
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 12 12'%3E%3Cpath fill='%23333' d='M6 8L1 3h10z'/%3E%3C/svg%3E");
  background-repeat: no-repeat;
  background-position: right 10px center;
  padding-right: 30px;
}

.custom-select option {
  color: var(--FieldTextBrush);
  background: var(--ButtonBrush);
}
</style>