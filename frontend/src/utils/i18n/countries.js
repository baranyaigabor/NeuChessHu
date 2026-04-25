const englishNameOverrides = {
    bosnia_and_herzegovina: 'Bosnia and Herzegovina',
    east_timor: 'Timor-Leste',
    ivory_coast: 'Ivory Coast',
    saint_kitts_and_nevis: 'Saint Kitts and Nevis',
    saint_vincent_and_the_grenadines: 'Saint Vincent and the Grenadines',
    sao_tome_and_principe: 'Sao Tome and Principe',
    trinidad_and_tobago: 'Trinidad and Tobago',
}

const hungarianNameOverrides = 
{
    afghanistan: 'Afganisztán',
    albania: 'Albánia',
    algeria: 'Algéria',
    andorra: 'Andorra',
    angola: 'Angola',
    antigua_and_barbuda: 'Antigua és Barbuda',
    argentina: 'Argentína',
    armenia: 'Örményország',
    australia: 'Ausztrália',
    austria: 'Ausztria',
    azerbaijan: 'Azerbajdzsán',
    bahamas: 'Bahama-szigetek',
    bahrain: 'Bahrein',
    bangladesh: 'Banglades',
    barbados: 'Barbados',
    belarus: 'Fehéroroszország',
    belgium: 'Belgium',
    belize: 'Belize',
    benin: 'Benin',
    bhutan: 'Bhután',
    bolivia: 'Bolívia',
    bosnia_and_herzegovina: 'Bosznia-Hercegovina',
    botswana: 'Botswana',
    brazil: 'Brazília',
    brunei: 'Brunei',
    bulgaria: 'Bulgária',
    burkina_faso: 'Burkina Faso',
    burundi: 'Burundi',
    cabo_verde: 'Zöld-foki Köztársaság',
    cambodia: 'Kambodzsa',
    cameroon: 'Kamerun',
    canada: 'Kanada',
    central_african_republic: 'Közép-afrikai Köztársaság',
    chad: 'Csád',
    chile: 'Chile',
    china: 'Kína',
    colombia: 'Kolumbia',
    comoros: 'Comore-szigetek',
    congo: 'Kongó',
    costa_rica: 'Costa Rica',
    croatia: 'Horvátország',
    cuba: 'Kuba',
    cyprus: 'Ciprus',
    czechia: 'Csehország',
    denmark: 'Dánia',
    djibouti: 'Dzsibuti',
    dominica: 'Dominika',
    dominican_republic: 'Dominikai Köztársaság',
    east_timor: 'Kelet-Timor',
    ecuador: 'Ecuador',
    egypt: 'Egyiptom',
    el_salvador: 'Salvador',
    equatorial_guinea: 'Egyenlítői-Guinea',
    eritrea: 'Eritrea',
    estonia: 'Észtország',
    eswatini: 'Eswatini',
    ethiopia: 'Etiópia',
    fiji: 'Fidzsi-szigetek',
    finland: 'Finnország',
    france: 'Franciaország',
    gabon: 'Gabon',
    gambia: 'Gambia',
    georgia: 'Grúzia',
    germany: 'Németország',
    ghana: 'Ghána',
    greece: 'Görögország',
    grenada: 'Grenada',
    guatemala: 'Guatemala',
    guinea: 'Guinea',
    guinea_bissau: 'Bissau-Guinea',
    guyana: 'Guyana',
    haiti: 'Haiti',
    honduras: 'Honduras',
    hungary: 'Magyarország',
    iceland: 'Izland',
    india: 'India',
    indonesia: 'Indonézia',
    iran: 'Irán',
    iraq: 'Irak',
    ireland: 'Írország',
    israel: 'Izrael',
    italy: 'Olaszország',
    ivory_coast: 'Elefántcsontpart',
    jamaica: 'Jamaica',
    japan: 'Japán',
    jordan: 'Jordánia',
    kazakhstan: 'Kazahsztán',
    kenya: 'Kenya',
    kiribati: 'Kiribati',
    kosovo: 'Koszovó',
    kuwait: 'Kuvait',
    kyrgyzstan: 'Kirgizisztán',
    laos: 'Laosz',
    latvia: 'Lettország',
    lebanon: 'Libanon',
    lesotho: 'Lesotho',
    liberia: 'Libéria',
    libya: 'Líbia',
    liechtenstein: 'Liechtenstein',
    lithuania: 'Litvánia',
    luxembourg: 'Luxemburg',
    madagascar: 'Madagaszkár',
    malawi: 'Malawi',
    malaysia: 'Malajzia',
    maldives: 'Maldív-szigetek',
    mali: 'Mali',
    malta: 'Málta',
    marshall_islands: 'Marshall-szigetek',
    mauritania: 'Mauritánia',
    mauritius: 'Mauritius',
    mexico: 'Mexikó',
    micronesia: 'Mikronézia',
    moldova: 'Moldova',
    monaco: 'Monaco',
    mongolia: 'Mongólia',
    montenegro: 'Montenegró',
    morocco: 'Marokkó',
    mozambique: 'Mozambik',
    myanmar: 'Mianmar',
    namibia: 'Namíbia',
    nauru: 'Nauru',
    nepal: 'Nepál',
    netherlands: 'Hollandia',
    new_zealand: 'Új-Zéland',
    nicaragua: 'Nicaragua',
    niger: 'Niger',
    nigeria: 'Nigéria',
    north_korea: 'Észak-Korea',
    north_macedonia: 'Észak-Macedónia',
    norway: 'Norvégia',
    oman: 'Omán',
    pakistan: 'Pakisztán',
    palau: 'Palau',
    palestine: 'Palesztina',
    panama: 'Panama',
    papua_new_guinea: 'Pápua Új-Guinea',
    paraguay: 'Paraguay',
    peru: 'Peru',
    philippines: 'Fülöp-szigetek',
    poland: 'Lengyelország',
    portugal: 'Portugália',
    qatar: 'Katar',
    romania: 'Románia',
    russia: 'Oroszország',
    rwanda: 'Ruanda',
    saint_kitts_and_nevis: 'Saint Kitts és Nevis',
    saint_lucia: 'Saint Lucia',
    saint_vincent_and_the_grenadines: 'Saint Vincent és a Grenadine-szigetek',
    samoa: 'Szamoa',
    san_marino: 'San Marino',
    sao_tome_and_principe: 'São Tomé és Príncipe',
    saudi_arabia: 'Szaúd-Arábia',
    senegal: 'Szenegál',
    serbia: 'Szerbia',
    seychelles: 'Seychelle-szigetek',
    sierra_leone: 'Sierra Leone',
    singapore: 'Szingapúr',
    slovakia: 'Szlovákia',
    slovenia: 'Szlovénia',
    solomon_islands: 'Salamon-szigetek',
    somalia: 'Szomália',
    south_africa: 'Dél-afrikai Köztársaság',
    south_korea: 'Dél-Korea',
    south_sudan: 'Dél-Szudán',
    spain: 'Spanyolország',
    sri_lanka: 'Srí Lanka',
    sudan: 'Szudán',
    suriname: 'Suriname',
    sweden: 'Svédország',
    switzerland: 'Svájc',
    syria: 'Szíria',
    taiwan: 'Tajvan',
    tajikistan: 'Tádzsikisztán',
    tanzania: 'Tanzánia',
    thailand: 'Thaiföld',
    togo: 'Togo',
    tonga: 'Tonga',
    trinidad_and_tobago: 'Trinidad és Tobago',
    tunisia: 'Tunézia',
    turkey: 'Törökország',
    turkmenistan: 'Türkmenisztán',
    tuvalu: 'Tuvalu',
    uganda: 'Uganda',
    ukraine: 'Ukrajna',
    united_arab_emirates: 'Egyesült Arab Emírségek',
    united_kingdom: 'Egyesült Királyság',
    united_states: 'Egyesült Államok',
    uruguay: 'Uruguay',
    uzbekistan: 'Üzbegisztán',
    vanuatu: 'Vanuatu',
    vatican_city: 'Vatikán',
    venezuela: 'Venezuela',
    vietnam: 'Vietnám',
    yemen: 'Jemen',
    zambia: 'Zambia',
    zimbabwe: 'Zimbabwe',
}

const countryCodeOverrides = 
{
    cabo_verde: 'CV',
    cape_verde: 'CV',
    congo: 'CG',
    czechia: 'CZ',
    east_timor: 'TL',
    ivory_coast: 'CI',
    kosovo: 'XK',
    palestine: 'PS',
    russia: 'RU',
    sao_tome_and_principe: 'ST',
    syria: 'SY',
    taiwan: 'TW',
    turkey: 'TR',
    united_states: 'US',
    vatican_city: 'VA',
    vietnam: 'VN',
}

let englishRegionCodeBySlug = null

function slugify(value) 
{
    return value.normalize('NFD')
                .replace(/[\u0300-\u036f]/g, '')
                .replace(/&/g, ' and ')
                .replace(/[^a-zA-Z0-9]+/g, '_')
                .replace(/^the_/, '')
                .replace(/^_|_$/g, '')
                .toLowerCase()
}

function humanizeCountryKey(value) 
{
    if (englishNameOverrides[value]) 
    {
        return englishNameOverrides[value]
    }

    return value.split('_')
                .map((word) => (word === 'and' 
                    ? 'and' 
                    : word.charAt(0).toUpperCase() + word.slice(1)))
                .join(' ')
}

function getEnglishRegionCodeBySlug() 
{
    if (englishRegionCodeBySlug) 
    {
        return englishRegionCodeBySlug
    }

    englishRegionCodeBySlug = {}

    if (typeof Intl.supportedValuesOf !== 'function') 
    {
        return englishRegionCodeBySlug
    }

    try 
    {
        const displayNames = new Intl.DisplayNames(['en-US'], { type: 'region' })

        Intl.supportedValuesOf('region').forEach((code) => 
        {
            const name = displayNames.of(code)
            if (name) 
            {
                englishRegionCodeBySlug[slugify(name)] = code
            }
        })
    }
    catch 
    {

    }

    return englishRegionCodeBySlug
}

function getCountryCode(value) 
{
    if (countryCodeOverrides[value])
    {
        return countryCodeOverrides[value]
    }

    return getEnglishRegionCodeBySlug()[value]
}

export function countryName(value, currentLocale = 'en', hungarianFallback = '') 
{
    if (!value) 
    {
        return ''
    }

    if (currentLocale === 'hu' && hungarianFallback) 
    {
        return hungarianFallback
    }

    if (currentLocale === 'hu' && hungarianNameOverrides[value]) 
    {
        return hungarianNameOverrides[value]
    }

    const code = getCountryCode(value)

    if (code) 
    {
        try 
        {
            const locale = currentLocale === 'hu' ? 'hu-HU' : 'en-US'
            return new Intl.DisplayNames([locale], { type: 'region' }).of(code)
        } 
        catch 
        {

        }
    }

    return humanizeCountryKey(value)
}

export function countryValueFromStoredName(name, countries) 
{
    if (!name)
    {
        return ''
    }

    const found = countries.find((country) =>
        country.value === name ||
        country.name === name ||
        countryName(country.value, 'en') === name ||
        countryName(country.value, 'hu', country.name) === name
    )

    return found?.value ?? ''
}