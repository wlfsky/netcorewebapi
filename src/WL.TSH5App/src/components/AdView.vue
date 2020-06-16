<template>
    <van-row>
    <van-col span="24">{{adurl}}</van-col>
    </van-row>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
    data () {
        return {
          adurl: 'adurl'
        }
    },
    created () {
      this.adurl = '广告地址'
    }
})
</script>

<style scoped>

</style>
