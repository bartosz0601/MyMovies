<script>
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

export default {
  props: ["show", "item"],
  methods: {
    remove() {
      this.$emit("remove", this.item);
    },
  },
};
</script>

<template>
  <Transition name="modal">
    <div v-if="show" class="modal-mask">
      <div class="modal-container">
        <h3 class="text-center">Delete movie?</h3>
        <fieldset disabled>
          <div class="form-floating mb-2">
            <input
              type="text"
              class="form-control"
              id="disabledtitleInput"
              placeholder="title"
              v-bind:value="item.title"
            />
            <label for="titleInput">Title</label>
          </div>
          <div class="form-floating mb-2">
            <input
              type="text"
              class="form-control"
              id="directorInput"
              placeholder="director"
              v-bind:value="item.director"
            />
            <label for="directorInput">Director</label>
          </div>
          <div class="form-floating mb-2">
            <input
              type="text"
              class="form-control"
              id="yearInput"
              placeholder="year"
              v-bind:value="item.year"
            />
            <label for="yearInput">Year</label>
          </div>
          <div class="form-floating mb-2">
            <input
              type="text"
              class="form-control"
              id="rateInput"
              placeholder="rate"
              v-bind:value="item.rate"
            />
            <label for="rateInput">Rate</label>
          </div>
        </fieldset>
        <button
          type="button"
          class="btn btn-danger float-start"
          @click="remove"
        >
          Delete
        </button>
        <button
          type="button"
          class="btn btn-secondary float-end"
          @click="$emit('close')"
        >
          Close
        </button>
      </div>
    </div>
  </Transition>
</template>

<style>
.modal-mask {
  position: fixed;
  z-index: 1000;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  transition: opacity 0.3s ease;
}

.modal-container {
  width: 400px;
  margin: auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
}

.modal-enter-from {
  opacity: 0;
}

.modal-leave-to {
  opacity: 0;
}

.modal-enter-from .modal-container,
.modal-leave-to .modal-container {
  transform: scale(1.1);
}
</style>
