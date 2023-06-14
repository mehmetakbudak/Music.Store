<template>
  <div class="card">
    <div class="card-header bg-white py-3">
      <el-page-header @back="cancel">
        <template #content>
          <span class="text-large font-600 mr-3"> {{ title }} </span>
        </template>
      </el-page-header>
    </div>
    <div class="card-body">
      <el-row class="p-3">
        <el-col :md="12" :sm="24">
          <el-form
            label-position="top"
            :model="form"
            class="pt-0"
            ref="formRef"
            :rules="rules"
          >
            <el-form-item label="Title" class="w-100" prop="title">
              <el-input v-model="form.title" size="large"/>
            </el-form-item>
            <el-form-item label="Artist" class="w-100" prop="artistId">
              <el-select
                class="w-100"
                v-model="form.artistId"
                filterable
                placeholder="Select Artist"
                size="large"
              >
                <el-option
                  v-for="item in artists"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="Image">
              <el-upload
                ref="uploadRef"
                class="upload-demo w-100"
                :show-file-list="true"
                :auto-upload="false"
                :limit="1"
                :on-change="handleChange"
              >
                <template #trigger>
                  <el-button type="primary">
                    <el-icon class="me-2"><Upload /></el-icon> Select
                    Image File</el-button
                  >
                </template>
              </el-upload>
              <el-image
                style="width: 200px; height: 200px; margin: 15px 0"
                v-if="form.imageUrl"
                :src="form.imageUrl"
                :zoom-rate="1.2"
                fit="cover"
                :preview-src-list="srcList"
              />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="save(formRef)">Save</el-button>
              <el-button @click="cancel">Cancel</el-button>
            </el-form-item>
          </el-form>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script lang="ts" setup>
import axios from "axios";
import type {
  FormInstance,
  FormRules,
  UploadInstance,
  UploadProps,
} from "element-plus";
import { ref, reactive } from "vue";
import router from "@/router";
import { Upload } from "@element-plus/icons-vue";
const formRef = ref<FormInstance>();
const uploadRef = ref<UploadInstance>();
let artists = ref<any>([]);
let srcList = ref<any>([]);
let title = ref<string>("");

const rules = reactive<FormRules>({
  title: [
    { required: true, message: "First name is required.", trigger: "blur" },
    {
      max: 160,
      message: "The title must be a maximum of 160 characters.",
      trigger: "blur",
    },
  ],
  artistId: [
    { required: true, message: "Artist must select.", trigger: "blur" },
  ],
});

let form = reactive({
  id: 0,
  title: "",
  artistId: null,
  image: {},
  imageUrl: "",
});

initial();
getArtists();

function initial() {
  const id: any = router.currentRoute.value?.params?.id;
  if (id) {
    getById(id);
    title.value = "Album Edit";
  } else {
    title.value = "New Album";
    Object.assign(form, null);
  }
}

function getById(id: any) {
  axios.get(`${process.env.VUE_APP_URL}Album/${id}`).then((res: any) => {
    srcList = [];
    srcList.push(res.data.imageUrl);
    Object.assign(form, res.data);
  });
}

function getArtists() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/Artist`).then((res: any) => {
    artists.value = res.data;
  });
}

const handleChange: UploadProps["onChange"] = (uploadFile) => {
  form.image = uploadFile.raw!;
};

const save = async (formEl: FormInstance | undefined) => {
  if (!formEl) {
    return;
  }

  await formEl.validate((valid, fields) => {
    if (valid) {
      if (form.id == 0) {
        axios
          .post(`${process.env.VUE_APP_URL}Album`, form, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then((res: any) => {
            router.push("/album");
          });
      } else {
        axios
          .put(`${process.env.VUE_APP_URL}Album`, form, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then((res: any) => {
            router.push("/admin/albums");
          });
      }
    }
  });
};

const cancel = () => {
  router.push("/admin/albums");
};
</script>
