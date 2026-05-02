<template>
  <div class="task-list">
    <div class="filters">
      <input v-model="search" @input="$emit('filter', { status, search })" placeholder="Buscar por título ou descrição..." class="search-input" />
      <select v-model="status" @change="$emit('filter', { status, search })" class="status-select">
        <option value="">Todos os Status</option>
        <option value="Pendente">Pendente</option>
        <option value="EmAndamento">Em andamento</option>
        <option value="Concluido">Concluído</option>
      </select>
    </div>

    <div v-if="loading" class="loading">Carregando tarefas...</div>
    <div v-else-if="tasks.length === 0" class="empty">Nenhuma tarefa encontrada.</div>

    <div v-else class="tasks">
      <div v-for="task in tasks" :key="task.id" class="task-card" :class="task.status.toLowerCase()">
        <div class="task-info">
          <h4>{{ task.title }}</h4>
          <p>{{ task.description }}</p>
          <span class="badge">{{ formatStatus(task.status) }}</span>
          <small>{{ new Date(task.createdAt).toLocaleDateString() }}</small>
        </div>
        <div class="task-actions">
          <button @click="$emit('edit', task)" class="btn-edit">Editar</button>
          <button @click="$emit('delete', task.id)" class="btn-delete" :disabled="task.status === 'Concluido'">Excluir</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';

const props = defineProps(['tasks', 'loading']);
defineEmits(['edit', 'delete', 'filter']);

const search = ref('');
const status = ref('');

const formatStatus = (status) => {
  const map = {
    'Pendente': 'Pendente',
    'EmAndamento': 'Em andamento',
    'Concluido': 'Concluído'
  };
  return map[status] || status;
};
</script>

<style scoped>
.filters {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}
.search-input {
  flex: 1;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
}
.status-select {
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
}
.task-card {
  border: 1px solid #ddd;
  padding: 15px;
  border-radius: 8px;
  margin-bottom: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: white;
  border-left: 5px solid #ccc;
}
.task-card.pendente { border-left-color: #ffc107; }
.task-card.emandamento { border-left-color: #17a2b8; }
.task-card.concluido { border-left-color: #28a745; opacity: 0.8; }

.task-info h4 { margin: 0 0 5px 0; }
.task-info p { margin: 0 0 10px 0; color: #666; }
.badge {
  background: #eee;
  padding: 2px 8px;
  border-radius: 12px;
  font-size: 0.8em;
  margin-right: 10px;
}
.task-actions {
  display: flex;
  gap: 5px;
}
.btn-edit, .btn-delete {
  padding: 5px 10px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
.btn-edit { background: #007bff; color: white; }
.btn-delete { background: #dc3545; color: white; }
.btn-delete:disabled { background: #ccc; cursor: not-allowed; }

.loading, .empty {
  text-align: center;
  padding: 20px;
  color: #666;
}
</style>
