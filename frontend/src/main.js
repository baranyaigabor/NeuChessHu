import { createApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import { plugin, defaultConfig } from '@formkit/vue'
import {router} from '@/router'
import App from '@/App.vue'

import '@assets/main.css'

createApp(App)
  .use(createPinia().use(piniaPluginPersistedstate))
  .use(plugin, defaultConfig)
  .use(router)
  .mount('#app')
