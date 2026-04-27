<script setup>
import { computed, ref, watch } from 'vue'
import { DateFormatter, getLocalTimeZone, today, CalendarDate } from '@internationalized/date'
import { CalendarIcon } from 'lucide-vue-next'
import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import { Calendar } from '@/components/ui/calendar'
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover'
import { useI18n } from '@utils/i18n'

const props = defineProps({
    modelValue: {
        type: [String, Object],
        default: null
    }
})

function parseDate(val) 
{
    if (!val || val === 'Unknown')
    {
        return null
    }

    if (typeof val.toDate === 'function')
    {
        return val
    }

    const parts = String(val).split('-')

    if (parts.length !== 3)
    {
        return null
    }

    return new CalendarDate(Number(parts[0]), Number(parts[1]), Number(parts[2]))
}

function formatDate(val)
{
    if (!val)
    {
        return null
    }

    return `${val.year}-${String(val.month).padStart(2, '0')}-${String(val.day).padStart(2, '0')}`
}

const defaultPlaceholder = today(getLocalTimeZone())
const date = ref(parseDate(props.modelValue))
const { locale, t } = useI18n()

const dateFormatter = computed(() => new DateFormatter(locale.value === 'hu' ? 'hu-HU' : 'en-US', {
    dateStyle: 'long',
}))

const emit = defineEmits(['dateOfBirthChange'])

watch(date, (val) => {
    emit('dateOfBirthChange', formatDate(val))
})
</script>

<template>
    <div class="col">
        <label class=" m-0 mb-1" for="calendar">{{ t('common.dateOfBirth') }}:</label>

        <Popover id="calendar" v-slot="{ close }">

            <PopoverTrigger as-child class="w-full! bg-(--ButtonBrush)! rounded-[5px]! border-(--BorderBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]">
                <Button variant="outline"
                    :class="cn('w-60 justify-start text-left font-normal text-(--FieldTextBrush) border-(--BorderBrush)!', !date && 'text-(--FieldTextBrush) opacity-60')"
                    style="border-color: var(--BorderBrush);">

                    <CalendarIcon />
                    {{ date ? dateFormatter.format(date.toDate(getLocalTimeZone())) : t('common.pickDate') }}
                </Button>
            </PopoverTrigger>

            <PopoverContent class="w-auto p-0" align="start">
                <Calendar v-model="date" :default-placeholder="defaultPlaceholder" layout="month-and-year" initial-focus @update:model-value="close"/>
            </PopoverContent>

        </Popover>
    </div>
</template>