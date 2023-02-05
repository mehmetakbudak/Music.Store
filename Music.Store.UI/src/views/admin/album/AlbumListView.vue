<template>
  <div class="card">
    <div class="card-header bg-white py-3">
      <el-row>
        <el-col :span="20">
          <h3>Albums</h3>
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
                    to="/admin/albums/add"
                    ><el-icon><Plus /></el-icon> New Album</router-link
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
                  <el-form-item label="Title" class="w-100" prop="title">
                    <el-input
                      placeholder="Title"
                      v-model="filter.title"
                      clearable
                    />
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
                <el-form-item>
                  <el-button type="primary" @click="filterAlbum()"
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

      <el-table
        :data="data?.list"
        v-loading="loading"
        :stripe="true"        
      >
        <el-table-column type="expand">
          <template #default="props">
            <div class="p-3">
              <h5>Tracks</h5>
              <el-table :data="props.row.tracks" :stripe="true">
                <el-table-column prop="name" label="Name" min-width="200" />
                <el-table-column prop="composer" label="Composer" min-width="200"/>
                <el-table-column prop="genre.name" label="Genre" min-width="200"/>
                <el-table-column prop="mediaType.name" label="Media Type" min-width="200"/>
                <el-table-column prop="milliseconds" label="Milliseconds" />
                <el-table-column prop="unitPrice" label="Unit Price" />
              </el-table>
            </div>
          </template>
        </el-table-column>
        <el-table-column width="60" fixed="right">
          <template #default="props">
            <el-dropdown  trigger="click">
              <el-button type="primary" class="p-2">
                <el-icon><Setting /></el-icon>
              </el-button>
              <template #dropdown>
                <el-dropdown-menu>
                  <el-dropdown-item>
                    <router-link
                      :to="`/admin/albums/update/${props.row.id}`"
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
        <el-table-column width="75">
          <template #default="props">
            <img
              v-if="props.row.imageUrl"
              :src="props.row.imageUrl"
              height="50"
              width="50"
            />
          </template>
        </el-table-column>
        <el-table-column prop="title" label="Title" min-width="300" />
        <el-table-column prop="artist.name" label="Artist" min-width="300" />
      </el-table>

      <el-scrollbar>
        <el-pagination
          class="my-3"
          background
          layout="prev, pager, next, jumper, ->, total"
          :total="parseInt(data?.count)"
          :current-page="parseInt(data?.page)"
          :pager-count="5"
          :page-size="parseInt(data?.pageSize)"
          @currentChange="pageChange"
        />
      </el-scrollbar>
    </div>
  </div>
</template>
  
<script lang="ts" setup>
import { Plus, Setting } from "@element-plus/icons-vue";
import { ref, reactive } from "vue";
import { FormInstance, ElNotification, ElMessageBox } from "element-plus";
import axios from "axios";
import router from "@/router";
const filterFormRef = ref<FormInstance>();
const formRef = ref<FormInstance>();
let data = ref<any>([]);
let artists = ref<any>([]);
let loading = ref<boolean>(true);

const filter = reactive({
  title: "",
  artistIds: [],
  page: 1,
  pageSize: 5,
});

getAll();
getArtists();

function getAll() {
  axios
    .post(`${process.env.VUE_APP_URL}Album/GetByFilter`, filter)
    .then((res: any) => {
      data.value = res.data;
      loading.value = false;
    });
}

function getArtists() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/Artist`).then((res: any) => {
    artists.value = res.data;
  });
}

const pageChange = (e: any) => {
  filter.page = e;
  loading.value = true;
  getAll();
};

const filterAlbum = () => {
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
    axios.delete(`${process.env.VUE_APP_URL}Album/${id}`).then(() => {
      filter.page = 1;
      getAll();
      ElNotification({
        type: "success",
        title: "Success",
        message: "Album successfully deleted.",
      });
    });
  });
};
</script>