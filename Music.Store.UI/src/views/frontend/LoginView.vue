<template>
  <section
    class="breadcumb-area bg-img bg-overlay"
    :style="{ backgroundImage: `url(${imagePath})` }"
  >
    <div class="bradcumbContent">
      <p>See what’s new</p>
      <h2>Login</h2>
    </div>
  </section>
  <!-- ##### Breadcumb Area End ##### -->

  <!-- ##### Login Area Start ##### -->
  <section class="login-area section-padding-100">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-12 col-lg-8">
          <div class="login-content">
            <el-tabs type="border-card" class="demo-tabs">
              <el-tab-pane label="LOGIN">
                <!-- Login Form -->
                <div class="login-form p-5">
                  <form>
                    <div class="form-group mb-4">
                      <label class="form-label">Email Address</label>
                      <el-input
                        placeholder="Email Address"
                        style="height: 50px"
                      />
                    </div>
                    <div class="form-group">
                      <label class="form-label">Password</label>
                      <el-input
                        type="password"
                        placeholder="Password"
                        style="height: 50px"
                        show-password
                      />
                    </div>
                    <button type="submit" class="btn oneMusic-btn mt-30">
                      Login
                    </button>
                  </form>
                </div>
              </el-tab-pane>
              <el-tab-pane label="REGISTER">
                <form class="p-5">
                  <div class="form-group mb-4">
                    <label class="form-label">Name</label>
                    <el-input placeholder="Name" style="height: 50px" />
                  </div>
                  <div class="form-group mb-4">
                    <label class="form-label">Surname</label>
                    <el-input placeholder="Surname" style="height: 50px" />
                  </div>
                  <div class="form-group mb-4">
                    <label class="form-label">Email Address</label>
                    <el-input
                      placeholder="Email Address"
                      style="height: 50px"
                    />
                  </div>
                  <div class="form-group mb-4">
                    <label class="form-label">Password</label>
                    <el-input
                      type="password"
                      placeholder="Password"
                      style="height: 50px"
                      show-password
                    />
                  </div>
                  <div class="form-group mb-4">
                    <label class="form-label">Re-Password</label>
                    <el-input
                      type="password"
                      placeholder="Password"
                      style="height: 50px"
                      show-password
                    />
                  </div>
                  <button type="submit" class="btn oneMusic-btn mt-30">
                    Register
                  </button>
                </form>
              </el-tab-pane>
            </el-tabs>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- ##### Login Area End ##### -->
</template>

<script lang="ts" setup>
import imagePath from "@/assets/img/bg-img/breadcumb3.jpg";
</script>