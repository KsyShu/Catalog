'use strict';
const crypto = require('crypto');
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    return queryInterface.bulkInsert('employees', [{
      fullname: 'Администратор',
      username: 'admin',
      password: crypto.createHash('sha256').update('admin').digest('base64'),
      createdAt: new Date(),
      updatedAt: new Date()
    }])
  },

  async down (queryInterface, Sequelize) {
    return queryInterface.bulkDelete('employees', null, {});
  }
};
