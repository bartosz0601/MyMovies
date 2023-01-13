<script>
import agent from './api/agent';
import MoviesHeader from './components/MoviesHeader.vue';
import MoviesItem from './components/MoviesItem.vue';
import ModalForm from './components/ModalForm.vue';
import ModalDelete from './components/ModalDelete.vue';
import { v4 as uuidv4 } from 'uuid';
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"

export default {
  components: {
    MoviesItem: MoviesItem,
    ModalForm: ModalForm,
    ModalDelete: ModalDelete
  },
  data() {
    return {
      movies: [],
      showModalMovie: false,
      showModalDelete: false,
      choosenItem: {}
    }
  },
  methods: {
    initItem() {
      this.choosenItem = {title: "", director: "", year: 0, rate: 0};
      this.showModalMovie = true;
    },
    editItemClicked(id) {
      this.choosenItem = {...this.movies.find(x => x.id === id)};
      this.showModalMovie = true;
    },
    removeItemClicked(id) {
      this.choosenItem = {...this.movies.find(x => x.id === id)};
      this.showModalDelete = true;
    },
    async createMovie(item) {
      item.id = uuidv4();
      await agent.Movies.post(item);
      this.movies.push(item);
      console.log(item);
    },
    async editMovie(item) {
      await agent.Movies.put(item);
      let index = this.movies.findIndex(x => x.id === item.id);
      this.movies[index] = item;
    },
    async removeMovie(item) {
      console.log(item);
      await agent.Movies.delete(item.id);
      this.movies = this.movies.filter(x => x.id !== item.id);
    }
  },
  async mounted() {
    this.movies = await agent.Movies.getList();
  },
}
</script>

<template>
  <div class="container">
    <div>
      <button class="btn btn-primary mb-2" @click="initItem">Add new</button>
    </div>
    <Teleport to="body">
      <ModalForm v-bind:show="showModalMovie" v-bind:item="choosenItem" 
      @close="showModalMovie = false"
      @edit="editMovie"  
      @create="createMovie"/>
      <ModalDelete v-bind:show="showModalDelete" v-bind:item="choosenItem" 
      @close="showModalDelete = false"
      @remove="removeMovie"/>
      
    </Teleport>
    <div>
      <MoviesItem v-if="movies != undefined" v-for="item in movies" v-bind:key="item.id" v-bind:item="item"
        @editClicked="editItemClicked"
        @removeClicked="removeItemClicked" />
    </div>
  </div>
</template>

<style>

</style>
