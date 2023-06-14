import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'

import { createApp } from "vue";
import ElementPlus from "element-plus";
import { QuillEditor } from '@vueup/vue-quill'

import '@vueup/vue-quill/dist/vue-quill.snow.css';
import "element-plus/dist/index.css";
import "@/assets/css/classy-nav.css";
import "@/assets/css/audioplayer.css";
import "@/assets/css/theme.css";
import 'vue3-carousel/dist/carousel.css';
import "./assets/css/style.css";

import * as ElementPlusIconsVue from "@element-plus/icons-vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from "axios";
import VueAxios from "vue-axios";

const app = createApp(App);
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component);
}

app.component('QuillEditor', QuillEditor)

app.use(ElementPlus);
app.use(VueAxios, axios);
app.use(store);
app.use(router);
app.mount("#app");
