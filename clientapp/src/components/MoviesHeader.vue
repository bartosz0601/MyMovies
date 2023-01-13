<script setup>
import agent from '../api/agent';
import { ref } from 'vue'

    const a = ref(0); 
    const movie = ref(1);
    a.value = 123; 
    movie.value = await agent.Movies.get('43cb9d55-a10e-4edf-aaea-fd86ade7f6e6');

</script>
<template>
<h1>My movies app</h1>
<h2>{{ movie }}</h2>
</template>
<style>
</style>