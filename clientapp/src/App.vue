<script>
import agent from "./api/agent";
import Toast from "./components/Toast.vue";
import MoviesItem from "./components/MoviesItem.vue";
import ModalForm from "./components/ModalForm.vue";
import ModalDelete from "./components/ModalDelete.vue";
import { v4 as uuidv4 } from "uuid";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

export default {
  components: {
    MoviesItem: MoviesItem,
    ModalForm: ModalForm,
    ModalDelete: ModalDelete,
    Toast: Toast,
  },
  data() {
    return {
      movies: [],
      showModalMovie: false,
      showModalDelete: false,
      choosenItem: {},
      errorMessage: "",
      showToast: false,
      loadIndicatorExternal: false,
    };
  },
  methods: {
    initItem() {
      this.choosenItem = { title: "", director: "", year: 0, rate: 0 };
      this.showModalMovie = true;
    },
    editItemClicked(id) {
      this.choosenItem = { ...this.movies.find((x) => x.id === id) };
      this.showModalMovie = true;
    },
    removeItemClicked(id) {
      this.choosenItem = { ...this.movies.find((x) => x.id === id) };
      this.showModalDelete = true;
    },
    async createMovie(item) {
      try {
        //item.id = uuidv4();
        const newItem = await agent.Movies.post(item);
        this.showModalMovie = false;
        this.movies.push(newItem);
      } catch (e) {
        this.showError(e);
      }
    },
    async editMovie(item) {
      try {
        await agent.Movies.put(item);
        this.showModalMovie = false;
        let index = this.movies.findIndex((x) => x.id === item.id);  
        this.movies[index] = item;
      } catch (e) {
        this.showError(e);
      }
    },
    async removeMovie(item) {
      try {
        await agent.Movies.delete(item.id);
        this.movies = this.movies.filter((x) => x.id !== item.id);
        this.showModalDelete = false;
      } catch (e) {
        this.showError(e);
      }
    },
    async getExternalApi() {
      try {
        this.loadIndicatorExternal = true;
        const newMovies = await agent.Movies.getExternalApi();
        this.movies.push(...newMovies);
        this.loadIndicatorExternal = false;
      } catch (e) {
        this.showError(e);
        this.loadIndicatorExternal = false;
      }
    },
    showError(e) {
      this.errorMessage = e;
      this.showToast = !this.showToast;
    },
  },
  async mounted() {
    try {
      this.movies = await agent.Movies.getList();
    } catch (e) {
      this.showError(e);
    }
  },
};
</script>

<template>
  <Toast v-bind:show="showToast">
    {{ errorMessage }}
  </Toast>
  <div class="container mt-2">
    <div>
      <button class="btn btn-primary mb-2" @click="initItem">Add new</button>
      <button
        v-bind:class="[
          'btn btn-primary mb-2 mx-2',
          loadIndicatorExternal ? 'disabled' : '',
        ]"
        @click="getExternalApi"
      >
        <span
          v-if="loadIndicatorExternal"
          class="spinner-border spinner-border-sm"
          role="status"
          aria-hidden="true"
        ></span>
        Load movies
      </button>
    </div>
    <Teleport to="body">
      <ModalForm
        v-bind:show="showModalMovie"
        v-bind:inItem="choosenItem"
        @close="showModalMovie = false"
        @edit="editMovie"
        @create="createMovie"
      />
      <ModalDelete
        v-bind:show="showModalDelete"
        v-bind:item="choosenItem"
        @close="showModalDelete = false"
        @remove="removeMovie"
      />
    </Teleport>
    <div>
      <MoviesItem
        v-if="this.movies.length > 0"
        v-for="item in movies"
        v-bind:key="item.id"
        v-bind:item="item"
        @editClicked="editItemClicked"
        @removeClicked="removeItemClicked"
      />
      <p v-else>Click add movie or load movies form external API</p>
    </div>
  </div>
</template>

<style></style>
