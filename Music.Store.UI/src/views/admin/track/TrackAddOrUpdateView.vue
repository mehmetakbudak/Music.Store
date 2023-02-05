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
      <div class="p-3">
        <el-form
          label-position="top"
          :model="form"
          class="pt-0"
          ref="formRef"
          :rules="rules"
        >
          <div class="row">
            <div class="col-md-6">
              <el-form-item label="Name" prop="name">
                <el-input v-model="form.name" size="large" />
              </el-form-item>
            </div>
            <div class="col-md-6">
              <el-form-item label="Composer" prop="composer">
                <el-input v-model="form.composer" size="large" />
              </el-form-item>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <el-form-item label="Album" prop="albumId" class="mb-xs-3">
                <el-select
                  class="w-100"
                  v-model="form.albumId"
                  filterable
                  placeholder="Select Album"
                  size="large"
                >
                  <el-option
                    v-for="item in albums"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
            </div>
            <div class="col-md-6">
              <el-form-item label="Genre" prop="genreId" class="mb-xs-3">
                <el-select
                  class="w-100"
                  v-model="form.genreId"
                  filterable
                  placeholder="Select Genre"
                  size="large"
                >
                  <el-option
                    v-for="item in genres"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
            </div>
          </div>

          <div class="row">
            <div class="col-md-6">
              <el-form-item
                label="Media Type"
                prop="mediaTypeId"
                class="mb-xs-3"
              >
                <el-select
                  class="w-100"
                  v-model="form.mediaTypeId"
                  filterable
                  size="large"
                  placeholder="Select Media Type"
                >
                  <el-option
                    v-for="item in mediaTypes"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
            </div>
            <div class="col-md-6">
              <el-form-item label="Unit Price" prop="unitPrice">
                <el-input-number
                  v-model="form.unitPrice"
                  :min="1"
                  :precision="2"
                  :step="0.1"
                  controls-position="right"
                  size="large"
                />
              </el-form-item>
            </div>
          </div>
          <el-form-item label="Web Url" prop="webUrl">
            <el-input
              v-model="form.webUrl"
              :rows="2"
              type="textarea"
              placeholder="Web Url"
            />
          </el-form-item>
          <div class="row">
            <div class="col-12">
              <el-form-item label="Lyrics" prop="lyrics">
                <QuillEditor
                  content-type="html"
                  v-model:content="form.lyrics"
                  theme="snow"
                  :toolbar="toolbarOptions"
                  class="w-100"
                />
              </el-form-item>
            </div>
          </div>
          <el-form-item label="Audio File">
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
                  <el-icon class="me-2"><Upload /></el-icon> Select Audio
                  File</el-button
                >
              </template>
            </el-upload>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="save(formRef)">Save</el-button>
            <el-button @click="cancel">Cancel</el-button>
          </el-form-item>
        </el-form>
      </div>
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
import { ToolbarOptions } from "../../../models/consts/editor.js";
import router from "@/router";
import { Upload } from "@element-plus/icons-vue";
const formRef = ref<FormInstance>();
const uploadRef = ref<UploadInstance>();
let albums = ref<any>([]);
let genres = ref<any>([]);
let mediaTypes = ref<any>([]);
let title = ref<string>("");
const toolbarOptions = ToolbarOptions;

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
  name: "",
  composer: "",
  lyrics: "",
  albumId: null,
  artistId: null,
  mediaTypeId: null,
  genreId: null,
  unitPrice: null,
  file: {},
  fileUrl: "",
  webUrl: "",
});

initial();
getAlbums();
getMediaTypes();
getGenres();

function initial() {
  const id: any = router.currentRoute.value?.params?.id;
  if (id) {
    getById(id);
    title.value = "Track Edit";
  } else {
    title.value = "New Track";
    Object.assign(form, null);
  }
}

function getById(id: any) {
  axios.get(`${process.env.VUE_APP_URL}Track/${id}`).then((res: any) => {
    Object.assign(form, res.data);
  });
}

function getAlbums() {
  axios.get(`${process.env.VUE_APP_URL}Lookup/Album`).then((res: any) => {
    albums.value = res.data;
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

const handleChange: UploadProps["onChange"] = (uploadFile) => {
  form.file = uploadFile.raw!;
};

const save = async (formEl: FormInstance | undefined) => {
  if (!formEl) {
    return;
  }

  await formEl.validate((valid, fields) => {
    if (valid) {
      if (form.id == 0) {
        axios
          .post(`${process.env.VUE_APP_URL}Track`, form, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then((res: any) => {
            router.push("/admin/tracks");
          });
      } else {
        axios
          .put(`${process.env.VUE_APP_URL}Track`, form, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then((res: any) => {
            router.push("/admin/tracks");
          });
      }
    }
  });
};

const cancel = () => {
  router.push("/admin/tracks");
};
</script>
