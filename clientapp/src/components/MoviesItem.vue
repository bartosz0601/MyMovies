<script>
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "bootstrap-icons/font/bootstrap-icons.css";

export default {
  props: ["item"],
  methods: {
    editItem() {
      this.$emit("editClicked", this.item.id);
    },
    removeItem() {
      this.$emit("removeClicked", this.item.id);
    },
  },
};
</script>
<template>
  <div class="card mb-2">
    <div class="card-body">
      <h6 class="card-title">{{ item.title }}</h6>
      <p class="card-text">
        {{ item.year }}
        {{ item.director }}
        <i v-if="item.rate" class="bi bi-star"></i> {{ item.rate }}
      </p>
      <button type="button" class="btn btn-primary" @click="editItem">
        Edit
      </button>
      <button type="button" class="btn btn-danger ms-2" @click="removeItem">
        Delete
      </button>
    </div>
  </div>
</template>
<style></style>
