<template>
  <div class="card">
    <div class="card-header bg-white py-3">
      <el-row>
        <el-col :span="20">
          <h3>Customers</h3>
        </el-col>
        <el-col :span="4">
          <el-button
            class="btn btn-primary btn-sm float-end"
            :icon="Plus"
            type="primary"
            @click="add(formRef)"
            >Add</el-button
          >
        </el-col>
      </el-row>
    </div>
    <div class="card-body">
      <div class="my-3">
        <el-collapse class="w-100 border" :accordion="true">
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
                  <el-form-item
                    label="First Name"
                    class="w-100"
                    prop="firstName"
                  >
                    <el-input v-model="filter.firstName" clearable />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Last Name" prop="lastName">
                    <el-input
                      v-model="filter.lastName"
                      class="w-100"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item
                    label="Email Address"
                    class="w-100"
                    prop="email"
                  >
                    <el-input v-model="filter.email" clearable />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Phone" class="w-100" prop="phone">
                    <el-input v-model="filter.phone" clearable />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Address" class="w-100" prop="address">
                    <el-input v-model="filter.address" clearable />
                  </el-form-item>
                </el-col>
                <el-col :md="6" :sm="24">
                  <el-form-item label="Company" class="w-100" prop="company">
                    <el-input v-model="filter.company" clearable />
                  </el-form-item>
                </el-col>
                <el-form-item>
                  <el-button type="primary" @click="filterCustomer()"
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

      <el-table :data="data?.list" border class="w-100" v-loading="loading">
        <el-table-column prop="firstName" label="Name" width="100" />
        <el-table-column prop="lastName" label="Surname" width="100" />
        <el-table-column prop="email" label="Email Address" width="180" />
        <el-table-column prop="phone" label="Phone" width="100" />
        <el-table-column prop="fax" label="Fax" width="100" />
        <el-table-column prop="address" label="Address" width="200" />
        <el-table-column prop="postalCode" label="Postal Code" width="120" />
        <el-table-column prop="country" label="Country" width="100" />
        <el-table-column prop="city" label="City" width="100" />
        <el-table-column prop="company" label="Company" width="200" />
        <el-table-column fixed="right" label="" width="100">
          <template #default="scope">
            <el-button
              bg
              class="p-2"
              type="primary"
              :icon="Edit"
              @click="edit(scope.row.id)"
              size="small"
            ></el-button>

            <el-popconfirm
              title="Are you sure to delete this?"
              confirm-button-text="Yes"
              cancel-button-text="No"
              icon-color="#626AEF"
              @confirm="remove(scope.row.id)"
            >
              <template #reference>
                <el-button
                  bg
                  class="p-2"
                  type="danger"
                  :icon="Delete"
                  size="small"
                ></el-button>
              </template>
            </el-popconfirm>
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

  <el-dialog
    v-model="modalVisible"
    :title="modalTitle"
    :width="modalWidth"
    top="3vh"
  >
    <el-form
      label-position="top"
      :model="form"
      class="pt-0"
      ref="formRef"
      :rules="rules"
    >
      <el-form-item label="First Name" class="w-100" prop="firstName">
        <el-input v-model="form.firstName" />
      </el-form-item>
      <el-form-item label="Last Name" class="w-100" prop="lastName">
        <el-input v-model="form.lastName" />
      </el-form-item>
      <el-form-item label="Email" class="w-100" prop="email">
        <el-input v-model="form.email" />
      </el-form-item>
      <el-form-item label="Phone" class="w-100" prop="phone">
        <el-input v-model="form.phone" />
      </el-form-item>
      <el-form-item label="Fax" class="w-100" prop="fax">
        <el-input v-model="form.fax" />
      </el-form-item>
      <el-form-item label="Address" class="w-100" prop="address">
        <el-input
          :autosize="{ minRows: 2, maxRows: 4 }"
          type="textarea"
          v-model="form.address"
        />
      </el-form-item>
      <el-form-item label="Postal Code" class="w-100" prop="postalCode">
        <el-input v-model="form.postalCode" />
      </el-form-item>
      <el-form-item label="Country" class="w-100" prop="country">
        <el-input v-model="form.country" />
      </el-form-item>
      <el-form-item label="City" class="w-100" prop="city">
        <el-input v-model="form.city" />
      </el-form-item>
      <el-form-item label="Company" class="w-100" prop="company">
        <el-input
          :autosize="{ minRows: 2, maxRows: 4 }"
          type="textarea"
          v-model="form.company"
        />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="modalVisible = false">Cancel</el-button>
        <el-button class="bg-primary" type="primary" @click="save(formRef)"
          >Save</el-button
        >
      </span>
    </template>
  </el-dialog>
</template>
  
  <script lang="ts" setup>
import { Delete, Edit, Plus } from "@element-plus/icons-vue";
import axios from "axios";
import type { FormInstance, FormRules } from "element-plus";
import { ref, reactive } from "vue";
import { ElNotification } from "element-plus";

let data = ref<any>([]);
let loading = ref<boolean>(true);
let modalTitle = ref<string>("");
let modalVisible = ref<boolean>(false);
const formRef = ref<FormInstance>();
const filterFormRef = ref<FormInstance>();
const modalWidth: string = window.screen.width > 768 ? "30%" : "95%";

const rules = reactive<FormRules>({
  firstName: [
    { required: true, message: "First name is required.", trigger: "blur" },
    {
      max: 20,
      message: "The first name must be a maximum of 20 characters.",
      trigger: "blur",
    },
  ],
  lastName: [
    { required: true, message: "Last name is required!", trigger: "blur" },
  ],
  email: [
    {
      type: "email",
      message: "Please input correct email address",
      trigger: ["blur"],
    },
  ],
});

const filter = reactive({
  firstName: "",
  lastName: "",
  email: "",
  phone: "",
  address: "",
  company: "",
  page: 1,
  pageSize: 5,
});

let form = reactive({
  id: 0,
  firstName: "",
  lastName: "",
  email: "",
  phone: "",
  fax: "",
  address: "",
  postalCode: "",
  country: "",
  city: "",
  company: "",
});

getAll();

function getAll() {
  axios
    .post(`${process.env.VUE_APP_URL}Customer/GetByFilter`, filter)
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

const filterCustomer = () => {
  getAll();
};

const resetFilter = (formEl: FormInstance | undefined) => {
  formEl?.resetFields();
  getAll();
};

const add = (formEl: FormInstance | undefined) => {
  modalVisible.value = true;
  modalTitle.value = "Customer Add";
  formEl?.resetFields();
  form.id = 0;
};

const edit = (id: number) => {
  modalVisible.value = true;
  modalTitle.value = "Customer Edit";
  axios.get(`${process.env.VUE_APP_URL}Customer/${id}`).then((res: any) => {
    Object.assign(form, res.data);
  });
};

const remove = (id: number) => {
  axios.delete(`${process.env.VUE_APP_URL}Customer/${id}`).then((res: any) => {
    loading.value = true;
    getAll();
    ElNotification({
      type: "success",
      title: "Success",
      message: "Customer successfully deleted.",
    });
  });
};

const save = async (formEl: FormInstance | undefined) => {
  if (!formEl) {
    return;
  }

  await formEl.validate((valid, fields) => {
    if (valid) {
      if (form.id == 0) {
        axios
          .post(`${process.env.VUE_APP_URL}Customer`, form)
          .then((res: any) => {
            loading.value = true;
            modalVisible.value = false;
            getAll();
            ElNotification({
              type: "success",
              title: "Success",
              message: "Customer successfully added.",
            });
          });
      } else {
        axios
          .put(`${process.env.VUE_APP_URL}Customer`, form)
          .then((res: any) => {
            loading.value = true;
            modalVisible.value = false;
            getAll();
            ElNotification({
              type: "success",
              title: "Success",
              message: "Customer successfully updated.",
            });
          });
      }
    }
  });
};
</script>
  