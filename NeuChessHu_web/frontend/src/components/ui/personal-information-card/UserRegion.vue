<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { countryName, countryValueFromStoredName, countryValues } from '@utils/i18n/countries'
import { SelectRoot, SelectTrigger, SelectPortal, SelectContent, SelectViewport, SelectItem, SelectItemText, SelectScrollUpButton, SelectScrollDownButton } from 'reka-ui'
import { ChevronDown, ChevronUp } from 'lucide-vue-next'

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
    countryValueFromStoredName(props.value, localizedCountries.value) ?? ''
)

const selectedLabel = computed(() =>
    localizedCountries.value.find(c => c.value === selectedCountryValue.value)?.label ?? ''
)

function onValueChange(val) {
    const selectedCountry = localizedCountries.value.find(c => c.value === val)
    emit('regionChange', selectedCountry?.label ?? val)
}
</script>

<template>
    <div class="col">
        <label class="m-0 mb-1" for="region">{{ t('common.region') }}:</label>
        <SelectRoot :model-value="selectedCountryValue" @update:model-value="onValueChange">
            <SelectTrigger id="region" class="flex h-8 w-full min-w-0 cursor-default items-center gap-1 rounded-[5px]! border border-[var(--BorderBrush)]! bg-[var(--ButtonBrush)] px-2.5 py-1 text-sm text-[var(--FieldTextBrush)] shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] outline-none focus:ring-1 focus:ring-[var(--CalendarAccentBrush)]">
                <span class="truncate">
                    {{ selectedLabel || t('common.chooseCountry') }}
                </span>
                <ChevronDown class="ml-auto shrink-0 size-3.5 opacity-60" />
            </SelectTrigger>

            <SelectPortal>
                <SelectContent side="bottom" :side-offset="4" position="popper" :avoid-collisions="true" class="z-[9999] w-[var(--reka-select-trigger-width)] overflow-hidden rounded-md border border-[var(--BorderBrush)]! bg-[var(--SideBarBrush)] text-[var(--FieldTextBrush)] shadow-lg">
                    <SelectScrollUpButton class="flex h-6 cursor-default items-center justify-center bg-[var(--SideBarBrush)] text-[var(--SelectIconBrush)]">
                        <ChevronUp class="size-3.5" />
                    </SelectScrollUpButton>

                    <SelectViewport class="max-h-[280px] overflow-y-auto p-1 [scrollbar-color:var(--SelectIconBrush)_var(--ButtonBrush)] [scrollbar-width:thin] [&::-webkit-scrollbar]:w-2 [&::-webkit-scrollbar-thumb]:rounded-md [&::-webkit-scrollbar-thumb]:border-2 [&::-webkit-scrollbar-thumb]:border-[var(--ButtonBrush)] [&::-webkit-scrollbar-thumb]:bg-[var(--SelectIconBrush)] [&::-webkit-scrollbar-thumb:hover]:bg-[var(--SelectIconHoverBrush)] [&::-webkit-scrollbar-track]:rounded-md [&::-webkit-scrollbar-track]:bg-[var(--ButtonBrush)]">
                        <SelectItem v-for="c in localizedCountries" :key="c.value" :value="c.value" class="flex cursor-default select-none items-center rounded px-2.5 py-1.5 text-sm outline-none data-[highlighted]:bg-[var(--CalendarHoverBrush)] data-[highlighted]:text-[var(--FieldTextBrush)] data-[state=checked]:bg-[var(--CalendarSelectedBrush)] data-[state=checked]:font-medium">
                            <SelectItemText>{{ c.label }}</SelectItemText>
                        </SelectItem>
                    </SelectViewport>

                    <SelectScrollDownButton class="flex h-6 cursor-default items-center justify-center bg-[var(--SideBarBrush)] text-[var(--SelectIconBrush)]">
                        <ChevronDown class="size-3.5" />
                    </SelectScrollDownButton>
                </SelectContent>
            </SelectPortal>
        </SelectRoot>
    </div>
</template>