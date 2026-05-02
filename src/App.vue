<template>
  <div class="container">
    <header>
      <div class="header-content">
        <h1>MagnaData - Gestão de Tarefas</h1>
        <p v-if="tasks.length > 0" class="task-counter">{{ tasks.length }} tarefas encontradas</p>
      </div>
      <button @click="showForm = true" class="btn btn-add" v-if="!showForm">Nova Tarefa</button>
    </header>

    <main>
      <div v-if="error" class="error-msg">
        {{ error }}
        <button @click="error = null">x</button>
      </div>

      <TaskForm 
        v-if="showForm" 
        :taskToEdit="taskToEdit" 
        @save="handleSave" 
        @cancel="cancelEdit" 
      />

      <TaskList 
        :tasks="tasks" 
        :loading="loading" 
        @edit="editTask" 
        @delete="deleteTask" 
        @filter="handleFilter"
      />
    </main>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from './services/api';
import TaskForm from './components/TaskForm.vue';
import TaskList from './components/TaskList.vue';

const tasks = ref([]);
const loading = ref(false);
const error = ref(null);
const showForm = ref(false);
const taskToEdit = ref(null);

const fetchTasks = async (filters = {}) => {
  loading.value = true;
  error.value = null;
  try {
    const response = await api.getTasks(filters.status, filters.search);
    tasks.value = response.data;
  } catch (err) {
    error.value = 'Erro ao carregar tarefas. Verifique se o Backend está rodando.';
    console.error(err);
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchTasks();
});

const handleSave = async (taskData) => {
  try {
    if (taskData.id) {
      await api.updateTask(taskData.id, taskData);
    } else {
      await api.createTask(taskData);
    }
    showForm.value = false;
    taskToEdit.value = null;
    fetchTasks();
  } catch (err) {
    error.value = err.response?.data || 'Erro ao salvar tarefa.';
  }
};

const editTask = (task) => {
  taskToEdit.value = { ...task };
  showForm.value = true;
  window.scrollTo(0, 0);
};

const deleteTask = async (id) => {
  if (confirm('Deseja realmente excluir esta tarefa?')) {
    try {
      await api.deleteTask(id);
      fetchTasks();
    } catch (err) {
      error.value = err.response?.data || 'Erro ao excluir tarefa.';
    }
  }
};

const cancelEdit = () => {
  showForm.value = false;
  taskToEdit.value = null;
};

const handleFilter = (filters) => {
  fetchTasks(filters);
};
</script>

<style>
body {
  font-family: Arial, sans-serif;
  background-color: #f4f7f6;
  margin: 0;
  color: #333;
}
.container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}
header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
  border-bottom: 2px solid #42b983;
  padding-bottom: 15px;
}
.header-content h1 {
  color: #2c3e50;
  margin: 0;
}
.task-counter {
  margin: 5px 0 0 0;
  color: #666;
  font-size: 0.9em;
}
.btn-add {
  background-color: #42b983;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
  font-weight: bold;
}
.error-msg {
  background-color: #f8d7da;
  color: #721c24;
  padding: 10px;
  border-radius: 4px;
  margin-bottom: 20px;
  display: flex;
  justify-content: space-between;
}
.error-msg button {
  background: none;
  border: none;
  color: #721c24;
  cursor: pointer;
  font-weight: bold;
}
</style>
