<template>
  <div class="row">
    <div class="col-md-4">
      <div class="mb-3">
        <el-collapse class="w-100 border" :accordion="true" model-value="1">
          <el-collapse-item name="1">
            <template #title>
              <h6 class="ps-3">Playlists</h6>
            </template>

            <div class="px-3 pt-1">
              <div class="mb-3">
                <div class="row">
                  <div class="col-9">
                    <el-input
                      placeholder="Search"
                      v-model="filterPlaylist.name"
                      :prefix-icon="Search"
                      @keyup="onFilterPlaylist"
                    />
                  </div>
                  <div class="col-3">
                    <el-button
                      class="float-end bg-primary"
                      :icon="Plus"                      
                      type="primary"
                      @click="add(formRef)"
                      >Add</el-button
                    >
                  </div>
                </div>
              </div>

              <el-table
                :data="playlists?.list"
                border
                class="w-100"
                highlight-current-row
              >
                <el-table-column prop="name" label="Name" />
                <el-table-column width="150">
                  <template #default="scope">
                    <el-button
                      bg
                      class="p-2"
                      type="primary"
                      :icon="Edit"
                      size="small"
                      @click="onSelectPlaylist(scope.row)"
                    ></el-button>
                    <el-button
                      bg
                      class="p-2"
                      type="danger"
                      :icon="Delete"
                      size="small"
                      @click="onSelectPlaylist(scope.row)"
                    ></el-button>
                    <el-button
                      bg
                      class="p-2"
                      type="warning"
                      :icon="Select"
                      size="small"
                      @click="onSelectPlaylist(scope.row)"
                    ></el-button>
                  </template>
                </el-table-column>
              </el-table>

              <el-scrollbar>
                <el-pagination
                  class="my-3"
                  background
                  layout="prev, pager, next"
                  :total="parseInt(playlists.count)"
                  :current-page="parseInt(playlists.page)"
                  :pager-count="5"
                  :page-size="parseInt(playlists.pageSize)"
                  @currentChange="pageChangePlaylist"
                />
              </el-scrollbar>
            </div>
          </el-collapse-item>
        </el-collapse>
      </div>
    </div>
    <div class="col-md-8">
      <div v-if="!showDetail">
        <div class="alert alert-info">Please select playlist!</div>
      </div>
      <div v-if="showDetail">
        <div class="card mb-3">
          <div class="card-header bg-white my-3">
            <h6>{{ selectedPlaylist.name }} - Selected Tracks</h6>
          </div>
          <div class="card-body">
            <el-table :data="playlistTracks?.list" border class="w-100">
              <el-table-column prop="name" label="Name" />
              <el-table-column prop="composer" label="Composer" />
              <el-table-column prop="genre.name" label="Genre" />
              <el-table-column prop="milliseconds" label="Milliseconds" />
              <el-table-column fixed="right" label="" width="55">
                <template #default="scope">
                  <el-button
                    bg
                    class="p-2"
                    type="danger"
                    :icon="Delete"
                    @click="removeTrack(scope.row.id)"
                    size="small"
                  ></el-button>
                </template>
              </el-table-column>
            </el-table>

            <el-scrollbar>
              <el-pagination
                class="my-3"
                background
                layout="prev, pager, next, jumper, ->, total"
                :total="parseInt(playlistTracks.count)"
                :current-page="parseInt(playlistTracks.page)"
                :pager-count="5"
                :page-size="parseInt(playlistTracks.pageSize)"
                @currentChange="onPageChangePlaylistTrack"
              />
            </el-scrollbar>
          </div>
        </div>

        <div class="card">
          <div class="card-header bg-white my-3"><h6>All Tracks</h6></div>
          <div class="card-body">
            <el-table :data="tracks?.list" border class="w-100">
              <el-table-column prop="name" label="Name" />
              <el-table-column prop="composer" label="Composer" />
              <el-table-column prop="genre.name" label="Genre" />
              <el-table-column prop="milliseconds" label="Milliseconds" />
              <el-table-column fixed="right" label="" width="55">
                <template #default="scope">
                  <el-button
                    bg
                    class="p-2"
                    type="primary"
                    :icon="Plus"
                    @click="addTrack(scope.row.id)"
                    size="small"
                  ></el-button>
                </template>
              </el-table-column>
            </el-table>

            <el-scrollbar>
              <el-pagination
                class="my-3"
                background
                layout="prev, pager, next, jumper, ->, total"
                :total="parseInt(tracks.count)"
                :current-page="parseInt(tracks.page)"
                :pager-count="5"
                :page-size="parseInt(tracks.pageSize)"
                @currentChange="onPageChangeAllTrack"
              />
            </el-scrollbar>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
    
<script lang="ts" setup>
import { Delete, Edit, Plus, Search, Select } from "@element-plus/icons-vue";
import { ref, reactive } from "vue";
import { FormInstance } from "element-plus";
import axios from "axios";
const filterFormRef = ref<FormInstance>();
const formRef = ref<FormInstance>();
let playlists = ref<any>([]);
let tracks = ref<any>([]);
let playlistTracks = ref<any>([]);
let showDetail = ref<boolean>(false);
let selectedPlaylist = ref<any>();

const filterPlaylist = reactive({
  name: "",
  artistIds: [],
  page: 1,
  pageSize: 10,
});

const filterSelectedTracks = reactive({
  name: "",
  playlistId: null,
  page: 1,
  pageSize: 5,
});

const filterAllTracks = reactive({
  name: "",
  page: 1,
  pageSize: 5,
});

getPlaylist();

function getPlaylist() {
  axios
    .post(`${process.env.VUE_APP_URL}Playlist/GetByFilter`, filterPlaylist)
    .then((res: any) => {
      playlists.value = res.data;
    });
}

function getAllTracks() {
  axios
    .post(`${process.env.VUE_APP_URL}Track/GetByFilter`, filterAllTracks)
    .then((res: any) => {
      tracks.value = res.data;
    });
}

function getSelectedTracks() {
  axios
    .post(
      `${process.env.VUE_APP_URL}Playlist/GetPlaylistTracks`,
      filterSelectedTracks
    )
    .then((res: any) => {
      playlistTracks.value = res.data;
    });
}

const pageChangePlaylist = (e: any) => {
  filterPlaylist.page = e;
  getPlaylist();
};

const onPageChangePlaylistTrack = (e: any) => {
  filterSelectedTracks.page = e;
  getSelectedTracks();
};

const onPageChangeAllTrack = (e: any) => {
  filterAllTracks.page = e;
  getAllTracks();
};

const onFilterPlaylist = (e: any) => {
  filterPlaylist.name = e.target.value;
  getPlaylist();
};

const onSelectPlaylist = (e: any) => {
  showDetail.value = true;
  selectedPlaylist.value = e;
  filterSelectedTracks.playlistId = e.id;
  getSelectedTracks();
  getAllTracks();
};

const removeTrack = (e: any) => {};

const addTrack = (e: any) => {};

const add = (formEl: FormInstance | undefined) => {};
</script>
  
  <style lang="scss" scoped></style>
  