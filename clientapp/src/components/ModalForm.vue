<script>
import agent from '../api/agent';
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"

export default {
  props: 
  ['show', 
    'item'],
  data() {
    return {
    }
  },
  methods: {
    submit(){
      if(this.item.hasOwnProperty('id'))
      {
        this.$emit('edit', this.item);
      }
      else
      {
        this.$emit('create', this.item);
      }
      this.$emit('close');
    }
  },
  computed: {
    returnHeader() {
      return this.item.hasOwnProperty('id') ? 'Edit movie' : 'Create new movie'
    }
  }
}
</script>

<template>
  <Transition name="modal">
    <div v-if="show" class="modal-mask">
      <div class="modal-container">
        <h3 class="text-center">{{ returnHeader }}</h3>
        <div class="form-floating mb-2">
          <input type="text" class="form-control" id="titleInput" placeholder="title" v-model="item.title">
          <label for="titleInput">Title</label>
        </div>
        <div class="form-floating mb-2">
          <input type="text" class="form-control" id="directorInput" placeholder="director" v-model="item.director">
          <label for="directorInput">Director</label>
        </div>
        <div class="form-floating mb-2">
          <input type="text" class="form-control" id="yearInput" placeholder="year" v-model="item.year">
          <label for="yearInput">Year</label>
        </div>
        <div class="form-floating mb-2">
          <input type="text" class="form-control" id="rateInput" placeholder="rate" v-model="item.rate">
          <label for="rateInput">Rate</label>
        </div>
        <button type="button" class="btn btn-primary float-start" @click="submit">Submit</button>
        <button type="button" class="btn btn-secondary float-end" @click="$emit('close')">Close</button>
      </div>
    </div>
  </Transition>
</template>

<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
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

/*
 * The following styles are auto-applied to elements with
 * transition="modal" when their visibility is toggled
 * by Vue.js.
 *
 * You can easily play with the modal transition by editing
 * these styles.
 */

.modal-enter-from {
  opacity: 0;
}

.modal-leave-to {
  opacity: 0;
}

.modal-enter-from .modal-container,
.modal-leave-to .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}
</style>

<!-- 
   <div class="modal-container">
        <div class="modal-header">
          <slot name="header">default header</slot>
        </div>

        <div class="modal-body">
          <lable>Title</lable>
          <input type="text" name="title" v-model="title">
          <br>
          <lable>Director</lable>
          <input type="text" name="director" v-model="director">
          <br>
          <lable>Year</lable>
          <input type="text" name="year" v-model="year">
          <br>
          <lable>Rate</lable>
          <input type="text" name="rate" v-model="rate">
        </div>
        <button @click="create">
          Submit
        </button>

        <div class="modal-footer">
          <slot name="footer">
            <button class="modal-default-button" @click="$emit('close')">OK</button>
          </slot>
        </div>
      </div>
-->