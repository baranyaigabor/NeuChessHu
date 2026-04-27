<script setup>
import { getLocalTimeZone, today } from "@internationalized/date";
import { createReusableTemplate, reactiveOmit, useVModel } from "@vueuse/core";
import { CalendarRoot, useDateFormatter, useForwardPropsEmits } from "reka-ui";
import { createYear, createYearRange, toDate } from "reka-ui/date";
import { SelectRoot, SelectTrigger, SelectPortal, SelectContent, SelectViewport, SelectItem, SelectItemText, SelectScrollUpButton, SelectScrollDownButton } from "reka-ui";
import { computed, toRaw } from "vue";
import { cn } from "@/lib/utils";
import { ChevronDown, ChevronUp } from "lucide-vue-next";
import { CalendarCell, CalendarCellTrigger, CalendarGrid, CalendarGridBody, CalendarGridHead, CalendarGridRow, CalendarHeadCell, CalendarHeader, CalendarHeading, CalendarNextButton, CalendarPrevButton } from ".";

const props = defineProps({
    defaultValue: { type: null, required: false },
    defaultPlaceholder: { type: null, required: false },
    placeholder: { type: null, required: false },
    pagedNavigation: { type: Boolean, required: false },
    preventDeselect: { type: Boolean, required: false },
    weekStartsOn: { type: Number, required: false },
    weekdayFormat: { type: String, required: false },
    calendarLabel: { type: String, required: false },
    fixedWeeks: { type: Boolean, required: false },
    maxValue: { type: null, required: false },
    minValue: { type: null, required: false },
    locale: { type: String, required: false },
    numberOfMonths: { type: Number, required: false },
    disabled: { type: Boolean, required: false },
    readonly: { type: Boolean, required: false },
    initialFocus: { type: Boolean, required: false },
    isDateDisabled: { type: Function, required: false },
    isDateUnavailable: { type: Function, required: false },
    dir: { type: String, required: false },
    nextPage: { type: Function, required: false },
    prevPage: { type: Function, required: false },
    modelValue: { type: null, required: false, default: undefined },
    multiple: { type: Boolean, required: false },
    disableDaysOutsideCurrentView: { type: Boolean, required: false },
    asChild: { type: Boolean, required: false },
    as: { type: null, required: false },
    class: { type: null, required: false },
    layout: { type: null, required: false, default: undefined },
    yearRange: { type: Array, required: false },
});

const emits = defineEmits(["update:modelValue", "update:placeholder"]);

const delegatedProps = reactiveOmit(props, "class", "layout", "placeholder");

const placeholder = useVModel(props, "placeholder", emits, {
    passive: true,
    defaultValue: props.defaultPlaceholder ?? today(getLocalTimeZone()),
});

const formatter = useDateFormatter(props.locale ?? "en");

const yearRange = computed(() => {
    return (
        props.yearRange ??
        createYearRange({
            start:
                props?.minValue ??
                (
                    toRaw(props.placeholder) ??
                    props.defaultPlaceholder ??
                    today(getLocalTimeZone())
                ).cycle("year", -100),

            end:
                props?.maxValue ??
                (
                    toRaw(props.placeholder) ??
                    props.defaultPlaceholder ??
                    today(getLocalTimeZone())
                ).cycle("year", 0),
        })
    );
});

const [DefineMonthTemplate, ReuseMonthTemplate] = createReusableTemplate();
const [DefineYearTemplate, ReuseYearTemplate] = createReusableTemplate();

const forwarded = useForwardPropsEmits(delegatedProps, emits);
</script>

<template>
    <DefineMonthTemplate v-slot="{ date }">
        <div>
            <SelectRoot :model-value="String(date.month)" @update:model-value="(val) => { placeholder = placeholder.set({ month: Number(val) }) }">
                <SelectTrigger class="flex h-8 w-full min-w-0 cursor-default items-center gap-1 rounded-[5px]! border border-[var(--BorderBrush)]! bg-[var(--ButtonBrush)] px-2 py-1 text-sm text-[var(--FieldTextBrush)] shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] outline-none focus:ring-1 focus:ring-[var(--CalendarAccentBrush)]">
                    <span class="truncate">
                        {{ formatter.custom(toDate(date), { month: "short" }) }}
                    </span>
                    <ChevronDown class="ml-auto shrink-0 size-3.5 opacity-60" />
                </SelectTrigger>

                <SelectPortal>
                    <SelectContent side="bottom" :side-offset="4" position="popper" :avoid-collisions="true" class="z-[9999] w-[var(--reka-select-trigger-width)] overflow-hidden rounded-md border border-[var(--BorderBrush)]! bg-[var(--SideBarBrush)] text-[var(--FieldTextBrush)] shadow-lg">
                        <SelectScrollUpButton class="flex h-6 cursor-default items-center justify-center bg-[var(--SideBarBrush)] text-[var(--SelectIconBrush)]">
                            <ChevronUp class="size-3.5" />
                        </SelectScrollUpButton>

                        <SelectViewport class="max-h-[280px] overflow-y-auto p-1 [scrollbar-color:var(--SelectIconBrush)_var(--ButtonBrush)] [scrollbar-width:thin] [&::-webkit-scrollbar]:w-2 [&::-webkit-scrollbar-thumb]:rounded-md [&::-webkit-scrollbar-thumb]:border-2 [&::-webkit-scrollbar-thumb]:border-[var(--ButtonBrush)] [&::-webkit-scrollbar-thumb]:bg-[var(--SelectIconBrush)] [&::-webkit-scrollbar-thumb:hover]:bg-[var(--SelectIconHoverBrush)] [&::-webkit-scrollbar-track]:rounded-md [&::-webkit-scrollbar-track]:bg-[var(--ButtonBrush)]">
                            <SelectItem v-for="month in createYear({ dateObj: date })" :key="month.toString()" :value="String(month.month)" class="flex cursor-default select-none items-center rounded px-2.5 py-1.5 text-sm outline-none data-[highlighted]:bg-[var(--CalendarHoverBrush)] data-[highlighted]:text-[var(--FieldTextBrush)] data-[state=checked]:bg-[var(--CalendarSelectedBrush)] data-[state=checked]:font-medium">
                                <SelectItemText>
                                    {{ formatter.custom(toDate(month), { month: "short" }) }}
                                </SelectItemText>
                            </SelectItem>
                        </SelectViewport>

                        <SelectScrollDownButton class="flex h-6 cursor-default items-center justify-center bg-[var(--SideBarBrush)] text-[var(--SelectIconBrush)]">
                            <ChevronDown class="size-3.5" />
                        </SelectScrollDownButton>
                    </SelectContent>
                </SelectPortal>
            </SelectRoot>
        </div>
    </DefineMonthTemplate>

    <DefineYearTemplate v-slot="{ date }">
        <div>
            <SelectRoot :model-value="String(date.year)" @update:model-value="(val) => { placeholder = placeholder.set({ year: Number(val) }) }">
                <SelectTrigger class="flex h-8 w-full min-w-0 cursor-default items-center gap-1 rounded-[5px]! border border-[var(--BorderBrush)]! bg-[var(--ButtonBrush)] px-2 py-1 text-sm text-[var(--FieldTextBrush)] shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] outline-none focus:ring-1 focus:ring-[var(--CalendarAccentBrush)]">
                    <span class="truncate">
                        {{ formatter.custom(toDate(date), { year: "numeric" }) }}
                    </span>
                    <ChevronDown class="ml-auto shrink-0 size-3.5 opacity-60" />
                </SelectTrigger>
              
                <SelectPortal>
                    <SelectContent side="bottom" :side-offset="4" position="popper" :avoid-collisions="true" class="z-[9999] w-[var(--reka-select-trigger-width)] overflow-hidden rounded-md border border-[var(--BorderBrush)]! bg-[var(--SideBarBrush)] text-[var(--FieldTextBrush)] shadow-lg">
                        <SelectScrollUpButton class="flex h-6 cursor-default items-center justify-center bg-[var(--SideBarBrush)] text-[var(--SelectIconBrush)]">
                            <ChevronUp class="size-3.5" />
                        </SelectScrollUpButton>
                      
                        <SelectViewport class="max-h-[280px] overflow-y-auto p-1 [scrollbar-color:var(--SelectIconBrush)_var(--ButtonBrush)] [scrollbar-width:thin] [&::-webkit-scrollbar]:w-2 [&::-webkit-scrollbar-thumb]:rounded-md [&::-webkit-scrollbar-thumb]:border-2 [&::-webkit-scrollbar-thumb]:border-[var(--ButtonBrush)] [&::-webkit-scrollbar-thumb]:bg-[var(--SelectIconBrush)] [&::-webkit-scrollbar-thumb:hover]:bg-[var(--SelectIconHoverBrush)] [&::-webkit-scrollbar-track]:rounded-md [&::-webkit-scrollbar-track]:bg-[var(--ButtonBrush)]">
                            <SelectItem v-for="year in yearRange" :key="year.toString()" :value="String(year.year)" class="flex cursor-default select-none items-center rounded px-2.5 py-1.5 text-sm outline-none data-[highlighted]:bg-[var(--CalendarHoverBrush)] data-[highlighted]:text-[var(--FieldTextBrush)] data-[state=checked]:bg-[var(--CalendarSelectedBrush)] data-[state=checked]:font-medium">
                                <SelectItemText>
                                    {{ formatter.custom(toDate(year), { year: "numeric" }) }}
                                </SelectItemText>
                            </SelectItem>
                        </SelectViewport>
                      
                        <SelectScrollDownButton class="flex h-6 cursor-default items-center justify-center bg-[var(--SideBarBrush)] text-[var(--SelectIconBrush)]">
                            <ChevronDown class="size-3.5" />
                        </SelectScrollDownButton>
                    </SelectContent>
                </SelectPortal>
            </SelectRoot>
        </div>
    </DefineYearTemplate>

    <CalendarRoot
        v-slot="{ grid, weekDays, date }"
        v-bind="forwarded"
        v-model:placeholder="placeholder"
        data-slot="calendar"
        :class="cn('p-3 bg-[var(--SideBarBrush)] text-[var(--TextBrush)] rounded-md !border-[var(--BorderBrush)]', props.class)"
    >
        <CalendarHeader class="pt-0">
            <nav
                class="flex items-center gap-1 bg-transparent absolute top-0 inset-x-0 !border-[var(--BorderBrush)] justify-between"
            >
                <CalendarPrevButton>
                    <slot name="calendar-prev-icon" />
                </CalendarPrevButton>
                <CalendarNextButton>
                    <slot name="calendar-next-icon" />
                </CalendarNextButton>
            </nav>

            <slot
                name="calendar-heading"
                :date="date"
                :month="ReuseMonthTemplate"
                :year="ReuseYearTemplate"
            >
                <template v-if="layout === 'month-and-year'">
                    <div class="flex items-center justify-center gap-1">
                        <ReuseMonthTemplate :date="date" />
                        <ReuseYearTemplate :date="date" />
                    </div>
                </template>
                <template v-else-if="layout === 'month-only'">
                    <div class="flex items-center justify-center gap-1">
                        <ReuseMonthTemplate :date="date" />
                        {{ formatter.custom(toDate(date), { year: "numeric" }) }}
                    </div>
                </template>
                <template v-else-if="layout === 'year-only'">
                    <div class="flex items-center justify-center gap-1">
                        {{ formatter.custom(toDate(date), { month: "short" }) }}
                        <ReuseYearTemplate :date="date" />
                    </div>
                </template>
                <template v-else>
                    <CalendarHeading />
                </template>
            </slot>
        </CalendarHeader>

        <div class="flex flex-col gap-y-4 mt-4 sm:flex-row sm:gap-x-4 sm:gap-y-0">
            <CalendarGrid v-for="month in grid" :key="month.value.toString()">
                <CalendarGridHead>
                    <CalendarGridRow>
                        <CalendarHeadCell v-for="day in weekDays" :key="day">
                            {{ day }}
                        </CalendarHeadCell>
                    </CalendarGridRow>
                </CalendarGridHead>
                <CalendarGridBody>
                    <CalendarGridRow
                        v-for="(weekDates, index) in month.rows"
                        :key="`weekDate-${index}`"
                        class="mt-2 w-full"
                    >
                        <CalendarCell
                            v-for="weekDate in weekDates"
                            :key="weekDate.toString()"
                            :date="weekDate"
                        >
                            <CalendarCellTrigger :day="weekDate" :month="month.value" />
                        </CalendarCell>
                    </CalendarGridRow>
                </CalendarGridBody>
            </CalendarGrid>
        </div>
    </CalendarRoot>
</template>