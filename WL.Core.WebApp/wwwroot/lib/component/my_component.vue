
<template>
<button class="my-c-class" v-on:click="count++">You clicked me {{ count }} times.333</button>
</template>

<script>
    module.exports = {
        data () {
            return {
                count: 0
            }
        }
    }
</script>


<style scoped>
.my-c-class{
    background-color: black;
    color: greenyellow;
}
</style>
