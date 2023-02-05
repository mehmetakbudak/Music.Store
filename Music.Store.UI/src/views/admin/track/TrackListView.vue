<template>
  <div class="card">
    <div class="card-header bg-white py-3">
      <el-row>
        <el-col :span="20">
          <h3>Tracks</h3>
        </el-col>
        <el-col :span="4">
          <el-dropdown :hide-on-click="false" class="float-end">
            <el-button size="large" type="primary">
              <el-icon class="me-1"><Setting /></el-icon> Operations<el-icon
                class="el-icon--right"
                ><arrow-down
              /></el-icon>
            </el-button>
            <template #dropdown>
              <el-dropdown-menu>
                <el-dropdown-item
                  ><router-link
                    class="text-decoration-none text-dark"
                    to="/admin/tracks/add"
                    ><el-icon><Plus /></el-icon> New Track</router-link
                  >
                </el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </el-col>
      </el-row>
    </div>
    <div class="card-body">
      <div class="my-3">
        <el-collapse class="w-100" :accordion="true">
          <el-collapse-item name="1">
            <template #title>
              <h5 class="ps-3">Filter</h5>
            </template>
            <div class="border-top p-3 pb-0">
              <el-form
                label-position="top"
                :model="filter"
                class="row"
                ref="filterFormRef"
              >
                <el-col :md="6" :sm="24">
                  <el-form-item label="Name" class="w-100" prop="name">
                    <el-input
                      placeholder="Name"
                      v-model="filter.name"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Composer" class="w-100" prop="composer">
                    <el-input
                      placeholder="Composer"
                      v-model="filter.composer"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Album" class="w-100" prop="albumIds">
                    <el-select
                      class="w-100"
                      v-model="filter.albumIds"
                      multiple
                      filterable
                      placeholder="Select Albums"
                    >
                      <el-option
                        v-for="item in albums"
                        :key="item.id"
                        :label="item.name"
                        :value="item.id"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Artist" class="w-100" prop="artistIds">
                    <el-select
                      class="w-100"
                      v-model="filter.artistIds"
                      multiple
                      filterable
                      placeholder="Select Artists"
                    >
                      <el-option
                        v-for="item in artists"
                        :key="item.id"
                        :label="item.name"
                        :value="item.id"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Genre" class="w-100" prop="genreIds">
                    <el-select
                      class="w-100"
                      v-model="filter.genreIds"
                      multiple
                      filterable
                      placeholder="Select Genres"
                    >
                      <el-option
                        v-for="item in genres"
                        :key="item.id"
                        :label="item.name"
                        :value="item.id"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item
                    label="Media Types"
                    class="w-100"
                    prop="mediaTypeIds"
                  >
                    <el-select
                      class="w-100"
                      v-model="filter.mediaTypeIds"
                      multiple
                      filterable
                      placeholder="Select Media Types"
                    >
                      <el-option
                        v-for="item in mediaTypes"
                        :key="item.id"
                        :label="item.name"
                        :value="item.id"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-form-item>
                  <el-button type="primary" @click="filterTrack()"
                    >Filter</el-button
                  >
                  <el-button @click="resetFilter(filterFormRef)"
                    >Reset</el-button
                  >
                </el-form-item>
              </el-form>
            </div>
          </el-collapse-item>
        </el-collapse>
      </div>

      <el-table :data="data?.list" :stripe="true" v-loading="loading">
        <el-table-column prop="name" label="Name" min-width="200" />
        <el-table-column prop="composer" label="Composer" min-width="200" />
        <el-table-column prop="album.title" label="Album" min-width="200" />
        <el-table-column
          prop="album.artist.name"
          label="Artist"
          min-width="200"
        />
        <el-table-column prop="genre.name" label="Genre" min-width="200" />
        <el-table-column
          prop="mediaType.name"
          label="Media Type"
          min-width="200"
        />
        <el-table-column prop="milliseconds" label="Milliseconds" />
        <el-table-column prop="unitPrice" label="Unit Price" />
        <el-table-column width="60" fixed="right">
          <template #default="props">
            <el-dropdown trigger="click">
              <el-button type="primary" class="p-2">
                <el-icon><Setting /></el-icon>
              </el-button>
              <template #dropdown>
                <el-dropdown-menu>
                  <el-dropdown-item>
                    <router-link
                      :to="`/admin/tracks/update/${props.row.id}`"
                      class="text-decoration-none text-dark"
                      >Edit</router-link
                    >
                  </el-dropdown-item>
                  <el-dropdown-item>
                    <a
                      @click="remove(props.row.id)"
                      class="text-decoration-none text-dark"
                      >Delete</a
                    >
                  </el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </template>
        </el-table-column>
      </el-table>

      <el-scrollbar>
        <el-pagination
          class="my-3"
          background
          layout="prev, pager, next, jumper, ->, total"
          :total="parseInt(data.count)"
          :current-page="parseInt(data.page)"
          :pager-count="5"
          :page-size="parseInt(data.pageSize)"
          @currentChange="pageChange"
        />
      </el-scrollbar>
    </div>
  </div>
</template>
    
  <script lang="ts" setup>
import { Plus, Setting } from "@element-plus/icons-vue";
import { ref, reactive } from "vue";
import { ElMessageBox, ElNotification, FormInstance } from "element-plus";
import axios from "axios";
import router from "@/router";
const filterFormRef = ref<FormInstance>();
const formRef = ref<FormInstance>();
let data = ref<any>([]);
let albums = ref<any>([]);
let artists = ref<any>([]);
let genres = ref<any>([]);
let mediaTypes = ref<any>([]);
let loading = ref<boolean>(true);

const filter = reactive({
  name: "",
  composer: "",
  albumIds: [],
  artistIds: [],
  genreIds: [],
  mediaTypeIds: [],
  page: 1,
  pageSize: 5,
});

getAll();
getAlbums();
getArtists();
getGenres();
getMediaTypes();

function getAll() {
  axios
    .post(`${process.env.VUE_APP_URL}Track/GetByFilter`, filter)
    .then((res: any) => {
      data.value = res.data;
      loading.value = false;
    });
}

function getAlbums() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/Album`).then((res: any) => {
    albums.value = res.data;
  });
}

function getArtists() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/Artist`).then((res: any) => {
    artists.value = res.data;
  });
}

function getGenres() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/Genre`).then((res: any) => {
    genres.value = res.data;
  });
}

function getMediaTypes() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/MediaType`).then((res: any) => {
    mediaTypes.value = res.data;
  });
}

const pageChange = (e: any) => {
  filter.page = e;
  loading.value = true;
  getAll();
};

const filterTrack = () => {
  getAll();
};

const resetFilter = (formEl: FormInstance | undefined) => {
  formEl?.resetFields();
  getAll();
};

const remove = (id: any) => {
  ElMessageBox.confirm("Are you sure you want to delete?", "Delete", {
    confirmButtonText: "OK",
    cancelButtonText: "Cancel",
    type: "info",
  }).then(() => {
    axios.delete(`${process.env.VUE_APP_URL}Track/${id}`).then(() => {
      filter.page = 1;
      getAll();
      ElNotification({
        type: "success",
        title: "Success",
        message: "Track successfully deleted.",
      });
    });
  });
};
</script>
  
  <style lang="scss" scoped></style>
  