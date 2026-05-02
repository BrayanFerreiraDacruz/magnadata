<template>
  <div class="task-form">
    <h3>{{ taskToEdit ? 'Editar Tarefa' : 'Nova Tarefa' }}</h3>
    <form @submit.prevent="saveTask">
      <div class="form-group">
        <label>Título:</label>
        <input v-model="form.title" required placeholder="Digite o título" class="form-control" />
      </div>
      <div class="form-group">
        <label>Descrição:</label>
        <textarea v-model="form.description" placeholder="Digite a descrição" class="form-control"></textarea>
      </div>
      <div v-if="taskToEdit" class="form-group">
        <label>Status:</label>
        <select v-model="form.status" class="form-control">
          <option value="Pendente">Pendente</option>
          <option value="EmAndamento">Em andamento</option>
          <option value="Concluido">Concluído</option>
        </select>
      </div>
      <div class="actions">
        <button type="submit" class="btn btn-primary">Salvar</button>
        <button type="button" @click="$emit('cancel')" class="btn btn-secondary">Cancelar</button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';

const props = defineProps(['taskToEdit']);
const emit = defineEmits(['save', 'cancel']);

const form = ref({
  title: '',
  description: '',
  status: 'Pendente'
});

onMounted(() => {
  if (props.taskToEdit) {
    form.value = { ...props.taskToEdit };
  }
});

watch(() => props.taskToEdit, (newVal) => {
  if (newVal) {
    form.value = { ...newVal };
  } else {
    form.value = { title: '', description: '', status: 'Pendente' };
  }
});

const saveTask = () => {
  emit('save', { ...form.value });
};
</script>

<style scoped>
.task-form {
  background: #f9f9f9;
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 20px;
  border: 1px solid #ddd;
}
.form-group {
  margin-bottom: 15px;
}
.form-group label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}
.form-control {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}
.actions {
  display: flex;
  gap: 10px;
}
.btn {
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
.btn-primary {
  background: #42b983;
  color: white;
}
.btn-secondary {
  background: #999;
  color: white;
}
</style>
