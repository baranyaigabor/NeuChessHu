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
        type: String,
        default: null
    }
})

const emit = defineEmits(['update:modelValue', 'dateOfBirthChange'])

function parseDate(val) 
{
    if (!val || val === 'Unknown')
    {
        return null
    }

    const parts = val.split('-')

    if (parts.length !== 3)
    {
        return null
    }

    return new CalendarDate(Number(parts[0]), Number(parts[1]), Number(parts[2]))
}

const defaultPlaceholder = today(getLocalTimeZone())
const date = ref(parseDate(props.modelValue))
const { locale, t } = useI18n()

const df = computed(() => new DateFormatter(locale.value === 'hu' ? 'hu-HU' : 'en-US', { dateStyle: 'long' }))

watch(date, (val) => 
{
    if (!val)
    {
        return
    }

    const str = `${val.year}-${String(val.month).padStart(2, '0')}-${String(val.day).padStart(2, '0')}`
    emit('update:modelValue', str)
    emit('dateOfBirthChange', str)
})
</script>

<template>
    <div class="w-full min-w-0">
        <Popover id="calendar" v-slot="{ close }">
            <PopoverTrigger as-child class="w-full! rounded-[5px]! border-black! bg-(--ButtonBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]">
                <Button variant="outline" :class="cn( 'w-full min-w-0 justify-start gap-2 overflow-hidden whitespace-nowrap text-left font-normal text-(--FieldTextBrush) border-black! px-1 md:px-3 [&_svg]:shrink-0', !date && 'text-[var(--FieldTextBrush)] opacity-60')" style="border-color: #000000;">
                    <CalendarIcon />
                    {{ date ? df.format(date.toDate(getLocalTimeZone())) : t('common.pickDate') }}
                </Button>
            </PopoverTrigger>

            <PopoverContent class="w-auto p-0" align="start">
                <Calendar v-model="date" :default-placeholder="defaultPlaceholder" layout="month-and-year" initial-focus @update:model-value="close"/>
            </PopoverContent>
        </Popover>
    </div>
</template>