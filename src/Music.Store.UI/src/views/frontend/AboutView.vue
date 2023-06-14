<template>
  <Carousel :settings="settings" :breakpoints="breakpoints" :wrapAround="true">
    <Slide v-for="slide in 10" :key="slide">
      <div class="carousel__item">{{ slide }}</div>
    </Slide>

    <template #addons>
      <Navigation />
      <Pagination />
    </template>
  </Carousel>
</template>

<script lang="ts" setup>
import { Carousel, Slide, Pagination, Navigation } from "vue3-carousel";

const settings = {
  itemsToShow: 1,
  snapAlign: "center",
};
const breakpoints = {
  700: {
    itemsToShow: 3.5,
    snapAlign: "center",
  },
  1024: {
    itemsToShow: 5,
    snapAlign: "start",
  },
};
</script>


<style>
.carousel__prev,
.carousel__next {
  box-sizing: content-box;
  border: 1px solid white;
  background-color: white;
  border-radius: 10px;
}
</style>