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
          </div>
          <div class="row">
            <div class="col-md-6">
              <el-form-item label="Popular" prop="isPopular">
                <el-switch v-model="form.isPopular" size="large" />
              </el-form-item>
            </div>
          </div>
          <div class="row">
            <div class="col-12">
              <el-form-item label="Bio" prop="bio">
                <QuillEditor
                  content-type="html"
                  v-model:content="form.bio"
                  theme="snow"
                  :toolbar="toolbarOptions"
                  class="w-100"
                />
              </el-form-item>
            </div>
          </div>
          <el-form-item label="Image" class="mb-3">
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
                  <el-icon class="me-2"><Upload /></el-icon> Select Image
                  File</el-button
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
          <el-form-item> </el-form-item>
        </el-form>
      </div>
    </div>
    <div class="card-footer py-3 bg-white">
      <el-button type="primary" @click="save(formRef)">Save</el-button>
      <el-button @click="cancel">Cancel</el-button>
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
let srcList = ref<any>([]);
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
  bio: "",
  image: {},
  imageUrl: "",
  isPopular: false,
});

initial();

function initial() {
  const id: any = router.currentRoute.value?.params?.id;
  if (id) {
    getById(id);
    title.value = "Artist Edit";
  } else {
    title.value = "New Artist";
    Object.assign(form, null);
  }
}

function getById(id: any) {
  axios.get(`${process.env.VUE_APP_URL}Artist/${id}`).then((res: any) => {
    srcList = [];
    srcList.push(res.data.imageUrl);
    Object.assign(form, res.data);
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
          .post(`${process.env.VUE_APP_URL}Artist`, form, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then((res: any) => {
            router.push("/admin/artists");
          });
      } else {
        axios
          .put(`${process.env.VUE_APP_URL}Artist`, form, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          })
          .then((res: any) => {
            router.push("/admin/artists");
          });
      }
    }
  });
};

const cancel = () => {
  router.push("/admin/artists");
};
</script>
