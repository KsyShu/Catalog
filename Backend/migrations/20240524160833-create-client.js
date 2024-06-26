'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('clients', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      fullname: {
        type: Sequelize.TEXT
      },
      username: {
        type: Sequelize.TEXT
      },
      phone: {
        type: Sequelize.INTEGER
      },
      address: {
        type: Sequelize.TEXT
      },
      password: {
        type: Sequelize.TEXT
        },
      category_id: {
        type: Sequelize.INTEGER,
        references: {
            model: {
              tableName: 'categories'
            },
            key: 'id'
        },
        allowNull: true
      },
      createdAt: {
        allowNull: false,
        type: Sequelize.DATE
      },
      updatedAt: {
        allowNull: false,
        type: Sequelize.DATE
      }
    });
  },
  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('clients');
  }
};