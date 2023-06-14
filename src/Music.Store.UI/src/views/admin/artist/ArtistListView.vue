<template>
  <div class="card">
    <div class="card-header bg-white py-3">
      <el-row>
        <el-col :span="20">
          <h3>Artists</h3>
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
                    to="/admin/artists/add"
                    ><el-icon><Plus /></el-icon> New Artist</router-link
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
                  <el-form-item label="Search Text" class="w-100" prop="text">
                    <el-input
                      placeholder="Search Text"
                      v-model="filter.text"
                      clearable
                      size="large"
                    />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Popular" class="w-100" prop="isPopular">
                    <el-select
                      class="w-100"
                      v-model="filter.isPopular"
                      placeholder="Popular"
                      clearable
                      size="large"
                    >
                      <el-option
                        v-for="item in popularList"
                        :key="item.key"
                        :label="item.label"
                        :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-form-item>
                  <el-button type="primary" @click="filterArtist()"
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

      <el-table :data="data?.list" v-loading="loading" :stripe="true">
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
                      :to="`/admin/artists/update/${props.row.id}`"
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
        <el-table-column width="120">
          <template #default="props">
            <img
              v-if="props.row.imageUrl"
              :src="props.row.imageUrl"
              height="100"
              width="100"
            />
          </template>
        </el-table-column>
        <el-table-column prop="name" label="Name" min-width="300" />
        <el-table-column prop="isPopular" label="Popular" min-width="300">
          <template #default="props">
            <el-switch v-model="props.row.isPopular" disabled></el-switch>
          </template>
        </el-table-column>
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

const popularList = ref([
  { key: 1, label: "All", value: null },
  { key: 1, label: "Yes", value: true },
  { key: 1, label: "No", value: false },
]);

const filter = reactive({
  text: "",
  isPopular: null,
  page: 1,
  pageSize: 5,
});

getAll();

function getAll() {
  axios
    .post(`${process.env.VUE_APP_URL}Artist/GetByFilter`, filter)
    .then((res: any) => {
      data.value = res.data;
      loading.value = false;
    });
}

const pageChange = (e: any) => {
  filter.page = e;
  loading.value = true;
  getAll();
};

const filterArtist = () => {
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
    axios.delete(`${process.env.VUE_APP_URL}Artist/${id}`).then(() => {
      filter.page = 1;
      getAll();
      ElNotification({
        type: "success",
        title: "Success",
        message: "Artist successfully deleted.",
      });
    });
  });
};
</script>