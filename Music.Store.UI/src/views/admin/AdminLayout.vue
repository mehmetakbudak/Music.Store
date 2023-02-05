
import { User } from '@element-plus/icons-vue';

<template>
  <nav class="navbar navbar-light bg-white border-bottom">
    <div class="container-fluid">
      <router-link class="navbar-brand" to="/admin">Admin Panel</router-link>
      <div class="d-flex">
        <el-dropdown :hide-on-click="false" class="float-end">
          <el-button type="primary">
            <el-icon class="me-1"><User /></el-icon> Username<el-icon
              class="el-icon--right"
              ><arrow-down
            /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item
                ><router-link class="text-decoration-none text-dark" to="/">
                  Go to Site</router-link
                >
              </el-dropdown-item>
              <el-dropdown-item
                ><router-link class="text-decoration-none text-dark" to="/">
                  Log out</router-link
                >
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </div>
    </div>
  </nav>
  <div class="container-fluid py-3">
    <div class="row">
      <div class="col-md-2">
        <el-menu
          default-active="2"
          class="el-menu-vertical-demo border mb-3"
          :collapse="isCollapse"
          @open="handleOpen"
          @close="handleClose"
        >
          <el-menu-item index="1" class="border-bottom">
            <template #title
              ><router-link
                class="text-decoration-none text-dark"
                to="/admin/customers"
              >
                Customers</router-link
              ></template
            >
          </el-menu-item>
          <el-menu-item index="2" class="border-bottom">
            <router-link
              class="text-decoration-none text-dark"
              to="/admin/albums"
            >
              Albums</router-link
            >
          </el-menu-item>
          <el-menu-item index="3" class="border-bottom">
            <template #title
              ><router-link
                class="text-decoration-none text-dark"
                to="/admin/artists"
              >
                Artists</router-link
              ></template
            >
          </el-menu-item>
          <el-menu-item index="4" class="border-bottom">
            <router-link
              class="text-decoration-none text-dark"
              to="/admin/tracks"
            >
              Tracks</router-link
            >
          </el-menu-item>
          <el-menu-item index="5">
            <router-link
              class="text-decoration-none text-dark"
              to="/admin/playlists"
            >
              Playlists</router-link
            >
          </el-menu-item>
        </el-menu>
      </div>
      <div class="col-md-10">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>


<script lang="ts" setup>
import { ref } from "vue";

const isCollapse = ref(false);

const handleOpen = (key: string, keyPath: string[]) => {
  console.log(key, keyPath);
};
const handleClose = (key: string, keyPath: string[]) => {
  console.log(key, keyPath);
};
</script>