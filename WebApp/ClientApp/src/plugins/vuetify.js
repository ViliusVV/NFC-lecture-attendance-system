import Vue from 'vue'
import {
  Vuetify,
  VApp,
  VNavigationDrawer,
  VFooter,
  VList,
  VBtn,
  VIcon,
  VGrid,
  VToolbar,
  VDataTable,
  VProgressLinear,
  VProgressCircular,
  transitions,
  VCalendar,
  VCard,
  VDialog,
  VForm,
  VTooltip,
  VImg,
  VTextField,
  VSheet,
  VDatePicker,
  VMenu,
  VSelect,
  VAutocomplete,
  VAvatar,
  VChip,
  VAlert,
  VExpansionPanel,
} from 'vuetify'
import 'vuetify/src/stylus/app.styl'
import lt from './lt-locale'

Vue.use(Vuetify, {
  components: {
    VApp,
    VNavigationDrawer,
    VFooter,
    VList,
    VBtn,
    VIcon,
    VGrid,
    VToolbar,
    VDataTable,
    VProgressLinear,
    VProgressCircular,
    transitions,
    VCalendar,
    VCard,
    VDialog,
    VForm,
    VTooltip,
    VImg,
    VTextField,
    VSelect,
    VMenu,
    VDatePicker,
    VSheet,
    VAutocomplete,
    VAvatar,
    VChip,
    VAlert,
    VExpansionPanel,
  },
  theme: {
    primary: '#1976D2',
    secondary: '#424242',
    accent: '#82B1FF',
    error: '#FF5252',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FFC107'
  },
  lang: {
    locales: { lt }
  },
})
