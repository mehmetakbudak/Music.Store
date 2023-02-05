import {
  createRouter,
  createWebHistory,
  RouteRecordRaw,
  RouterView,
} from "vue-router";
import HomeView from "../views/frontend/HomeView.vue";
import AboutView from "../views/frontend/AboutView.vue";
import AlbumView from "../views/frontend/AlbumView.vue";
import FrontendLayout from "../views/frontend/FrontendLayout.vue";
import LoginView from "../views/frontend/LoginView.vue";
import EventView from "../views/frontend/EventView.vue";
import NewsView from "../views/frontend/NewsView.vue";
import ContactView from "../views/frontend/ContactView.vue";

import CustomerView from "../views/admin/CustomerView.vue";
import AlbumListView from "../views/admin/album/AlbumListView.vue";
import AlbumAddOrUpdateView from "../views/admin/album/AlbumAddOrUpdateView.vue";
import TrackListView from "../views/admin/track/TrackListView.vue";
import PlaylistView from "../views/admin/PlaylistView.vue";
import AdminLayout from "../views/admin/AdminLayout.vue";
import DashboardView from "../views/admin/DashboardView.vue";
import TrackAddOrUpdateViewVue from "@/views/admin/track/TrackAddOrUpdateView.vue";
import ArtistListView from "@/views/admin/artist/ArtistListView.vue";
import ArtistAddOrUpdateView from "@/views/admin/artist/ArtistAddOrUpdateView.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    component: FrontendLayout,
    children: [
      {
        path: "/",
        component: HomeView,
      },
      {
        path: "/about",
        component: AboutView,
      },
      {
        path: "/album",
        component: AlbumView,
      },
      {
        path: "/login",
        component: LoginView,
      },
      {
        path: "/event",
        component: EventView,
      },
      {
        path: "/news",
        component: NewsView,
      },
      {
        path: "/contact",
        component: ContactView,
      },
    ],
  },
  {
    path: "/admin",
    component: AdminLayout,
    children: [
      {
        path: "/admin",
        component: DashboardView,
      },
      {
        path: "customers",
        component: CustomerView,
      },
      {
        path: "albums",
        component: AlbumListView,
      },
      {
        path: "albums/add",
        component: AlbumAddOrUpdateView,
      },
      {
        path: "albums/update/:id",
        component: AlbumAddOrUpdateView,
      },
      {
        path: "tracks",
        component: TrackListView,
      },
      {
        path: "tracks/add",
        component: TrackAddOrUpdateViewVue,
      },
      {
        path: "tracks/update/:id",
        component: TrackAddOrUpdateViewVue,
      },
      {
        path: "playlists",
        component: PlaylistView,
      },
      {
        path: "artists",
        component: ArtistListView,
      },
      {
        path: "artists/add",
        component: ArtistAddOrUpdateView,
      },
      {
        path: "artists/update/:id",
        component: ArtistAddOrUpdateView,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
