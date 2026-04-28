<script setup>
import { reactiveOmit } from "@vueuse/core";
import { CalendarCellTrigger, useForwardProps } from "reka-ui";
import { cn } from "@/lib/utils";
import { buttonVariants } from '@/components/ui/button';

const props = defineProps({
  day: { type: null, required: true },
  month: { type: null, required: true },
  asChild: { type: Boolean, required: false },
  as: { type: null, required: false, default: "button" },
  class: { type: null, required: false },
});

const delegatedProps = reactiveOmit(props, "class");

const forwardedProps = useForwardProps(delegatedProps);
</script>

<template>
  <CalendarCellTrigger
    data-slot="calendar-cell-trigger"
    :class="
      cn(
        buttonVariants({ variant: 'ghost' }),
        'size-8 p-0 font-normal aria-selected:opacity-100 cursor-default !text-[var(--TextBrush)] hover:!bg-[var(--CalendarHoverBrush)] hover:!text-[var(--TextBrush)] focus:!bg-[var(--CalendarHoverBrush)] focus:!text-[var(--TextBrush)] focus-visible:!ring-0',
        '[&[data-today]:not([data-selected])]:!bg-[var(--ButtonBrush)] [&[data-today]:not([data-selected])]:!text-[var(--TextBrush)] [&[data-today]:not([data-selected])]:!ring-1 [&[data-today]:not([data-selected])]:!ring-[var(--CalendarTodayBrush)] !border-[var(--BorderBrush)]',
        // Selected
        'data-[selected]:!bg-[var(--CalendarSelectedBrush)] data-[selected]:!text-[var(--TextBrush)] data-[selected]:!opacity-100 data-[selected]:hover:!bg-[var(--CalendarSelectedBrush)] data-[selected]:hover:!text-[var(--TextBrush)] data-[selected]:focus:!bg-[var(--CalendarSelectedBrush)] data-[selected]:focus:!text-[var(--TextBrush)]',
        // Disabled
        'data-[disabled]:text-[var(--TextMutedBrush)] data-[disabled]:opacity-50',
        // Unavailable
        'data-[unavailable]:text-[var(--StatusLossStrongBrush)] data-[unavailable]:line-through',
        // Outside months
        'data-[outside-view]:text-[var(--TextMutedBrush)]',
        props.class,
      )
    "
    v-bind="forwardedProps"
  >
    <slot />
  </CalendarCellTrigger> 
</template>
