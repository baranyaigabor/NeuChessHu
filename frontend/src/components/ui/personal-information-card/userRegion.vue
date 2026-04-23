<script setup>
import { computed, ref } from 'vue'
import { useI18n } from '@utils/i18n'
import { countryName } from '@utils/i18n/countries'

const props = defineProps({
  value: {
    type: String,
    default: ""
  }
})

const countries = ref([{ value: "afghanistan", name: "Afganisztán" },{ value: "albania", name: "Albánia" },{ value: "algeria", name: "Algéria" },{ value: "andorra", name: "Andorra" },{ value: "angola", name: "Angola" },{ value: "antigua_and_barbuda", name: "Antigua és Barbuda" },{ value: "argentina", name: "Argentína" },{ value: "armenia", name: "Örményország" },{ value: "australia", name: "Ausztrália" },{ value: "austria", name: "Ausztria" },{ value: "azerbaijan", name: "Azerbajdzsán" },{ value: "bahamas", name: "Bahama-szigetek" },{ value: "bahrain", name: "Bahrein" },{ value: "bangladesh", name: "Banglades" },{ value: "barbados", name: "Barbados" },{ value: "belarus", name: "Fehéroroszország" },{ value: "belgium", name: "Belgium" },{ value: "belize", name: "Belize" },{ value: "benin", name: "Benin" },{ value: "bhutan", name: "Bhután" },{ value: "bolivia", name: "Bolívia" },{ value: "bosnia_and_herzegovina", name: "Bosznia-Hercegovina" },{ value: "botswana", name: "Botswana" },{ value: "brazil", name: "Brazília" },{ value: "brunei", name: "Brunei" },{ value: "bulgaria", name: "Bulgária" },{ value: "burkina_faso", name: "Burkina Faso" },{ value: "burundi", name: "Burundi" },{ value: "cabo_verde", name: "Zöld-foki Köztársaság" },{ value: "cambodia", name: "Kambodzsa" },{ value: "cameroon", name: "Kamerun" },{ value: "canada", name: "Kanada" },{ value: "central_african_republic", name: "Közép-afrikai Köztársaság" },{ value: "chad", name: "Csád" },{ value: "chile", name: "Chile" },{ value: "china", name: "Kína" },{ value: "colombia", name: "Kolumbia" },{ value: "comoros", name: "Comore-szigetek" },{ value: "congo", name: "Kongó" },{ value: "costa_rica", name: "Costa Rica" },{ value: "croatia", name: "Horvátország" },{ value: "cuba", name: "Kuba" },{ value: "cyprus", name: "Ciprus" },{ value: "czechia", name: "Csehország" },{ value: "denmark", name: "Dánia" },{ value: "djibouti", name: "Dzsibuti" },{ value: "dominica", name: "Dominika" },{ value: "dominican_republic", name: "Dominikai Köztársaság" },{ value: "east_timor", name: "Kelet-Timor" },{ value: "ecuador", name: "Ecuador" },{ value: "egypt", name: "Egyiptom" },{ value: "el_salvador", name: "Salvador" },{ value: "equatorial_guinea", name: "Egyenlítői-Guinea" },{ value: "eritrea", name: "Eritrea" },{ value: "estonia", name: "Észtország" },{ value: "eswatini", name: "Eswatini" },{ value: "ethiopia", name: "Etiópia" },{ value: "fiji", name: "Fidzsi-szigetek" },{ value: "finland", name: "Finnország" },{ value: "france", name: "Franciaország" },{ value: "gabon", name: "Gabon" },{ value: "gambia", name: "Gambia" },{ value: "georgia", name: "Grúzia" },{ value: "germany", name: "Németország" },{ value: "ghana", name: "Ghána" },{ value: "greece", name: "Görögország" },{ value: "grenada", name: "Grenada" },{ value: "guatemala", name: "Guatemala" },{ value: "guinea", name: "Guinea" },{ value: "guinea_bissau", name: "Bissau-Guinea" },{ value: "guyana", name: "Guyana" },{ value: "haiti", name: "Haiti" },{ value: "honduras", name: "Honduras" },{ value: "hungary", name: "Magyarország" },{ value: "iceland", name: "Izland" },{ value: "india", name: "India" },{ value: "indonesia", name: "Indonézia" },{ value: "iran", name: "Irán" },{ value: "iraq", name: "Irak" },{ value: "ireland", name: "Írország" },{ value: "israel", name: "Izrael" },{ value: "italy", name: "Olaszország" },{ value: "ivory_coast", name: "Elefántcsontpart" },{ value: "jamaica", name: "Jamaica" },{ value: "japan", name: "Japán" },{ value: "jordan", name: "Jordánia" },{ value: "kazakhstan", name: "Kazahsztán" },{ value: "kenya", name: "Kenya" },{ value: "kiribati", name: "Kiribati" },{ value: "kosovo", name: "Koszovó" },{ value: "kuwait", name: "Kuvait" },{ value: "kyrgyzstan", name: "Kirgizisztán" },{ value: "laos", name: "Laosz" },{ value: "latvia", name: "Lettország" },{ value: "lebanon", name: "Libanon" },{ value: "lesotho", name: "Lesotho" },{ value: "liberia", name: "Libéria" },{ value: "libya", name: "Líbia" },{ value: "liechtenstein", name: "Liechtenstein" },{ value: "lithuania", name: "Litvánia" },{ value: "luxembourg", name: "Luxemburg" },{ value: "madagascar", name: "Madagaszkár" },{ value: "malawi", name: "Malawi" },{ value: "malaysia", name: "Malajzia" },{ value: "maldives", name: "Maldív-szigetek" },{ value: "mali", name: "Mali" },{ value: "malta", name: "Málta" },{ value: "marshall_islands", name: "Marshall-szigetek" },{ value: "mauritania", name: "Mauritánia" },{ value: "mauritius", name: "Mauritius" },{ value: "mexico", name: "Mexikó" },{ value: "micronesia", name: "Mikronézia" },{ value: "moldova", name: "Moldova" },{ value: "monaco", name: "Monaco" },{ value: "mongolia", name: "Mongólia" },{ value: "montenegro", name: "Montenegró" },{ value: "morocco", name: "Marokkó" },{ value: "mozambique", name: "Mozambik" },{ value: "myanmar", name: "Mianmar" },{ value: "namibia", name: "Namíbia" },{ value: "nauru", name: "Nauru" },{ value: "nepal", name: "Nepál" },{ value: "netherlands", name: "Hollandia" },{ value: "new_zealand", name: "Új-Zéland" },{ value: "nicaragua", name: "Nicaragua" },{ value: "niger", name: "Niger" },{ value: "nigeria", name: "Nigéria" },{ value: "north_korea", name: "Észak-Korea" },{ value: "north_macedonia", name: "Észak-Macedónia" },{ value: "norway", name: "Norvégia" },{ value: "oman", name: "Omán" },{ value: "pakistan", name: "Pakisztán" },{ value: "palau", name: "Palau" },{ value: "palestine", name: "Palesztina" },{ value: "panama", name: "Panama" },{ value: "papua_new_guinea", name: "Pápua Új-Guinea" },{ value: "paraguay", name: "Paraguay" },{ value: "peru", name: "Peru" },{ value: "philippines", name: "Fülöp-szigetek" },{ value: "poland", name: "Lengyelország" },{ value: "portugal", name: "Portugália" },{ value: "qatar", name: "Katar" },{ value: "romania", name: "Románia" },{ value: "russia", name: "Oroszország" },{ value: "rwanda", name: "Ruanda" },{ value: "saint_kitts_and_nevis", name: "Saint Kitts és Nevis" },{ value: "saint_lucia", name: "Saint Lucia" },{ value: "saint_vincent_and_the_grenadines", name: "Saint Vincent és a Grenadine-szigetek" },{ value: "samoa", name: "Szamoa" },{ value: "san_marino", name: "San Marino" },{ value: "sao_tome_and_principe", name: "São Tomé és Príncipe" },{ value: "saudi_arabia", name: "Szaúd-Arábia" },{ value: "senegal", name: "Szenegál" },{ value: "serbia", name: "Szerbia" },{ value: "seychelles", name: "Seychelle-szigetek" },{ value: "sierra_leone", name: "Sierra Leone" },{ value: "singapore", name: "Szingapúr" },{ value: "slovakia", name: "Szlovákia" },{ value: "slovenia", name: "Szlovénia" },{ value: "solomon_islands", name: "Salamon-szigetek" },{ value: "somalia", name: "Szomália" },{ value: "south_africa", name: "Dél-afrikai Köztársaság" },{ value: "south_korea", name: "Dél-Korea" },{ value: "south_sudan", name: "Dél-Szudán" },{ value: "spain", name: "Spanyolország" },{ value: "sri_lanka", name: "Srí Lanka" },{ value: "sudan", name: "Szudán" },{ value: "suriname", name: "Suriname" },{ value: "sweden", name: "Svédország" },{ value: "switzerland", name: "Svájc" },{ value: "syria", name: "Szíria" },{ value: "taiwan", name: "Tajvan" },{ value: "tajikistan", name: "Tádzsikisztán" },{ value: "tanzania", name: "Tanzánia" },{ value: "thailand", name: "Thaiföld" },{ value: "togo", name: "Togo" },{ value: "tonga", name: "Tonga" },{ value: "trinidad_and_tobago", name: "Trinidad és Tobago" },{ value: "tunisia", name: "Tunézia" },{ value: "turkey", name: "Törökország" },{ value: "turkmenistan", name: "Türkmenisztán" },{ value: "tuvalu", name: "Tuvalu" },{ value: "uganda", name: "Uganda" },{ value: "ukraine", name: "Ukrajna" },{ value: "united_arab_emirates", name: "Egyesült Arab Emírségek" },{ value: "united_kingdom", name: "Egyesült Királyság" },{ value: "united_states", name: "Egyesült Államok" },{ value: "uruguay", name: "Uruguay" },{ value: "uzbekistan", name: "Üzbegisztán" },{ value: "vanuatu", name: "Vanuatu" },{ value: "vatican_city", name: "Vatikán" },{ value: "venezuela", name: "Venezuela" },{ value: "vietnam", name: "Vietnám" },{ value: "yemen", name: "Jemen" },{ value: "zambia", name: "Zambia" },{ value: "zimbabwe", name: "Zimbabwe" }]);

const emit = defineEmits(['regionChange'])
const { locale, t } = useI18n()

const localizedCountries = computed(() =>
  countries.value.map((country) => ({
    ...country,
    label: countryName(country.value, locale.value, country.name),
  }))
)

function onRegionChange(event) 
{
  emit('regionChange', event.target.value)
}
</script>

<template>
    <div class="col">
        <label for="region">{{ t('common.region') }}:</label>
        <select :value="props.value" class="w-full m-0 p-1.5 ps-2 bg-(--ButtonBrush)! text-(--FieldTextBrush) rounded-[5px] border border-(--BorderBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] focus:bg-transparent focus:outline-none! custom-select" 
                @change="onRegionChange" 
                id="region" 
                name="region" 
                required>

            <option value="" disabled selected>{{ t('common.chooseCountry') }}</option>
            <option
              v-for="c in localizedCountries"
              :key="c.value"
              :value="c.value">
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
  max-height: 10rem;
  overflow-y: auto;
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